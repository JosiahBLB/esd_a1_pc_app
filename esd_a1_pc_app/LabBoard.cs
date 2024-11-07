using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace esd_a1_pc_app
{
    internal class LabBoard
    {
        // constants
        private const byte START_BYTE = 0x53;
        private const byte STOP_BYTE = 0xAA;
        private const byte TXCHECK = 0x00;
        private const byte READ_PINA = 0x01;
        private const byte READ_POT1 = 0x02;
        private const byte READ_POT2 = 0x03;
        private const byte READ_TEMP = 0X04;
        private const byte READ_LAMP = 0x05;
        private const byte SET_PORTC = 0X0A;
        private const byte SET_HEATER = 0X0B;
        private const byte SET_LIGHT = 0X0C;
        private const byte SET_MOTOR = 0X0D;
        public static readonly int TIMEOUT = 200;

        // private members
        private SerialPort _serialPort = new SerialPort();
        private Mutex _lock = new Mutex();

        public LabBoard()
        {
            initSerialPort();
        }

        // public
        public async Task<bool> connect()
        {
            bool connected = false;
            try
            {
                _serialPort.Open();
                connected = await checkTx();
            }
            catch (TimeoutException)
            {
                Debug.WriteLine("No response from serial port.");
                disconnect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                disconnect();
            }
            return connected;
        }

        public void disconnect()
        {
            _serialPort.Close();
        }

        public void setComPort(string comPort)
        {
            _serialPort.PortName = comPort;
        }

        public void setBaudrate(int baudrate)
        {
            _serialPort.BaudRate = baudrate;
        }

        public async Task<bool> checkTx()
        {
            int reply = await readUInt8(instruction: TXCHECK);
            return (reply == TXCHECK);
        }

        public async Task<int> readPinA()
        {
            return await readUInt8(instruction: READ_PINA);
        }

        public async Task<(float, float)> readPotV()
        {
            int pot1 = await readUInt8(instruction: READ_POT1);
            int pot2 = await readUInt8(instruction: READ_POT2);
            float v1 = 0.02f * pot1;
            float v2 = 0.02f * pot2;
            return (v1, v2);
        }

        public async Task<int> readLamp()
        {
            return await readUInt8(instruction: READ_LAMP);
        }

        public async Task<int> readTemp()
        {
            return await readUInt8(instruction: READ_TEMP);
        }

        public async Task<bool> writePortC(byte data)
        {
            int reply = await writeUInt16(instruction: SET_PORTC, data);
            return (reply == SET_PORTC);
        }

        public async Task<bool> writeLamp(UInt16 data)
        {
            int reply = await writeUInt16(instruction: SET_LIGHT, data);
            return (reply == SET_LIGHT);
        }

        public async Task<bool> enableHeater(bool enable)
        {
            int reply = await writeUInt16(instruction: SET_HEATER, (UInt16)(enable ? 1 : 0));
            return (reply == SET_HEATER);
        }

        public async Task<bool> writeMotor(UInt16 data)
        {
            int reply = await writeUInt16(instruction: SET_MOTOR, data);
            return (reply == SET_MOTOR);
        }

        public async Task<int> readUInt8(byte instruction)
        {

            return await Task.Run(() => { return write(instruction, 0, 0); }) ;
        }

        public async Task<int> writeUInt16(byte instruction, UInt16 data)
        {
            byte msb = (byte)(data >> 8);
            byte lsb = (byte)(data & (0xff));
            return await Task.Run(() => { return write(instruction, msb, lsb); });
        }

        // private
        private void initSerialPort()
        {
            _serialPort.BaudRate = 38400;
            _serialPort.Parity = Parity.None;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;
            _serialPort.WriteTimeout = 500;
            _serialPort.ReadTimeout = 500;
        }

        private int write(byte instruction, byte b1, byte b2)
        {
            byte[] output = { START_BYTE, instruction, b1, b2, STOP_BYTE };
            _serialPort.Write(output, 0, output.Length);
            Debug.WriteLine($"Sent: {output:X}.");
            Stopwatch sw = Stopwatch.StartNew();
            while (_serialPort.BytesToRead < 1) // Wait for a reply
            {
                if (sw.ElapsedMilliseconds > TIMEOUT)
                {
                    Debug.WriteLine("No reply received.");
                    throw new TimeoutException();
                }
            }
            int reply = _serialPort.ReadByte();
            Debug.WriteLine($"Reply:{reply:X}.");
            return reply;
        }
    }
}
