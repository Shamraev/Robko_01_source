using System;
using MCControl;

namespace BusControl
{
    class FrameFormer
    {
        public enum FrameIndexes
        {
            BEGIN,              //стартовый байт, 1 байт
            REQUEST_RESPONSE,   //байт запроса (ответа), 1 байт
            LENGTH,             //байт длины payload, 1 байт;
            OPERATION_CODE,     //байт кода операции, 1 байт;
            STATUS_CODE,        //байт кода статуса, 1 байт;
            PAYLOAD,            //байты полезной информации, байт;
            CRC                 //байты контрольной суммы, 2 байта.
        }
        public enum REQUEST_RESPONSE
        {
            REQUEST = 0,
            RESPONSE = 1
        }
        public enum OPERATION_CODE
        {
            NONE,
            MOVE_TO_ABSOLUTE_ANGLES_Q1Q2Q3,
            FIND_AND_GO_TO_ZEROS,
            GRIPPER_GRIP,                       //схват сжать до срабатывания датчика
            GRIPPER_UNGRIP,                     //схват разжать до срабатывания датчика + еще немного ??
            GRIPPER_OPEN_TO_ABSOLUTE_DISTANCE  //схват сжать губки на определенное расстояние между ними 
        }
        public enum STATUS_CODE
        {
            NONE,
            ERROR,
            DONE
        }

        public const byte FRAME_CRC_LEN = 2;
        public const byte FRAME_MIN_LEN = 7;
        public const byte FRAME_MAX_LEN = 32; //??
        public const byte DATA_LENGTH = 19;
        public const byte FRAME_LENGTH_WITHOUT_PAYLOAD = 7; //длина фрейма без полезной информации
                                                            // const byte FRAME_LENGTH_WITHOUT_CRC = DATA_LENGTH - FRAME_CRC_LEN;
        public const byte PAYLOAD_LENGTH = DATA_LENGTH - FRAME_LENGTH_WITHOUT_PAYLOAD;
        public const byte FRAME_BEGIN_VALUE = 1;
        public const byte FRAME_REQUEST_PAYLOAD_OFFSET = (byte)FrameIndexes.PAYLOAD; //??
        public const byte RESPONSE_FRAME_LENGTH = 9;        //??6??


        private MCController owner;
        public MCController Owner { get { return owner; } set { /*owner = value; */} }

        public byte[] DoFrame_Request_MoveToAbsoluteAngles(double q1, double q2, double q3)
        {
            var floatArray = new float[] { (float)Math.Round(q1, 2), (float)Math.Round(q2, 2), (float)Math.Round(q3, 2) };
            var Payload = new byte[PAYLOAD_LENGTH];
            Buffer.BlockCopy(floatArray, 0, Payload, 0, Payload.Length);

            if ((owner != null) && (owner.Owner.DoLog)) AddLog("FrameFormer.DoFrame_Request_MoveToAbsoluteAngles(): floatArray = " + string.Join(" ", floatArray));//??----

            byte[] Frame = new byte[FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length];
            Frame[(int)FrameIndexes.BEGIN] = FRAME_BEGIN_VALUE;
            Frame[(int)FrameIndexes.REQUEST_RESPONSE] = (byte)REQUEST_RESPONSE.REQUEST;
            Frame[(int)FrameIndexes.LENGTH] = (byte)(FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length);
            Frame[(int)FrameIndexes.OPERATION_CODE] = (byte)OPERATION_CODE.MOVE_TO_ABSOLUTE_ANGLES_Q1Q2Q3;
            Frame[(int)FrameIndexes.STATUS_CODE] = (byte)STATUS_CODE.NONE;
            //Buffer.BlockCopy(Payload, 0, Frame, FRAME_REQUEST_PAYLOAD_OFFSET, Frame.Length);
            Payload.CopyTo(Frame, FRAME_REQUEST_PAYLOAD_OFFSET);
            Calculate_CRC(Frame, Frame.Length - FRAME_CRC_LEN).CopyTo(Frame, Frame.Length - FRAME_CRC_LEN);

            return Frame;
        }

        public byte[] DoFrame_Request_FindAndGoToZeros()
        {
            var Payload = new byte[PAYLOAD_LENGTH];
            if ((owner != null) && (owner.Owner.DoLog)) AddLog("FrameFormer.DoFrame_Request_FindAndGoToZeros(): ");//??----

            byte[] Frame = new byte[FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length];
            Frame[(int)FrameIndexes.BEGIN] = FRAME_BEGIN_VALUE;
            Frame[(int)FrameIndexes.REQUEST_RESPONSE] = (byte)REQUEST_RESPONSE.REQUEST;
            Frame[(int)FrameIndexes.LENGTH] = (byte)(FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length);
            Frame[(int)FrameIndexes.OPERATION_CODE] = (byte)OPERATION_CODE.FIND_AND_GO_TO_ZEROS;
            Frame[(int)FrameIndexes.STATUS_CODE] = (byte)STATUS_CODE.NONE;
            Payload.CopyTo(Frame, FRAME_REQUEST_PAYLOAD_OFFSET);
            Calculate_CRC(Frame, Frame.Length - FRAME_CRC_LEN).CopyTo(Frame, Frame.Length - FRAME_CRC_LEN);

            return Frame;
        }
        public byte[] DoFrame_Request_GripperGrip()
        {
            var Payload = new byte[PAYLOAD_LENGTH];
            if ((owner != null) && (owner.Owner.DoLog)) AddLog("FrameFormer.DoFrame_Request_GripperGrip(): ");//??----

            byte[] Frame = new byte[FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length];
            Frame[(int)FrameIndexes.BEGIN] = FRAME_BEGIN_VALUE;
            Frame[(int)FrameIndexes.REQUEST_RESPONSE] = (byte)REQUEST_RESPONSE.REQUEST;
            Frame[(int)FrameIndexes.LENGTH] = (byte)(FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length);
            Frame[(int)FrameIndexes.OPERATION_CODE] = (byte)OPERATION_CODE.GRIPPER_GRIP;
            Frame[(int)FrameIndexes.STATUS_CODE] = (byte)STATUS_CODE.NONE;
            Payload.CopyTo(Frame, FRAME_REQUEST_PAYLOAD_OFFSET);
            Calculate_CRC(Frame, Frame.Length - FRAME_CRC_LEN).CopyTo(Frame, Frame.Length - FRAME_CRC_LEN);

            return Frame;
        }
        public byte[] DoFrame_Request_GripperUngrip()
        {
            var Payload = new byte[PAYLOAD_LENGTH];
            if ((owner != null) && (owner.Owner.DoLog)) AddLog("FrameFormer.DoFrame_Request_GripperUngrip(): ");//??----

            byte[] Frame = new byte[FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length];
            Frame[(int)FrameIndexes.BEGIN] = FRAME_BEGIN_VALUE;
            Frame[(int)FrameIndexes.REQUEST_RESPONSE] = (byte)REQUEST_RESPONSE.REQUEST;
            Frame[(int)FrameIndexes.LENGTH] = (byte)(FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length);
            Frame[(int)FrameIndexes.OPERATION_CODE] = (byte)OPERATION_CODE.GRIPPER_UNGRIP;
            Frame[(int)FrameIndexes.STATUS_CODE] = (byte)STATUS_CODE.NONE;
            Payload.CopyTo(Frame, FRAME_REQUEST_PAYLOAD_OFFSET);
            Calculate_CRC(Frame, Frame.Length - FRAME_CRC_LEN).CopyTo(Frame, Frame.Length - FRAME_CRC_LEN);

            return Frame;
        }
        
        public byte[] DoFrame_Request_GripperOpenToAbsoluteDistance(double a)
        {
            var Payload = new byte[PAYLOAD_LENGTH];
            Payload[0] = (byte)Math.Round(a, 2);//??
            //Buffer.BlockCopy(floatArray, 0, Payload, 0, Payload.Length);

            if ((owner != null) && (owner.Owner.DoLog)) AddLog("FrameFormer.DoFrame_Request_FindAndGoToZeros(): ");//??----

            byte[] Frame = new byte[FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length];
            Frame[(int)FrameIndexes.BEGIN] = FRAME_BEGIN_VALUE;
            Frame[(int)FrameIndexes.REQUEST_RESPONSE] = (byte)REQUEST_RESPONSE.REQUEST;
            Frame[(int)FrameIndexes.LENGTH] = (byte)(FRAME_LENGTH_WITHOUT_PAYLOAD + Payload.Length);
            Frame[(int)FrameIndexes.OPERATION_CODE] = (byte)OPERATION_CODE.GRIPPER_OPEN_TO_ABSOLUTE_DISTANCE;
            Frame[(int)FrameIndexes.STATUS_CODE] = (byte)STATUS_CODE.NONE;
            Payload.CopyTo(Frame, FRAME_REQUEST_PAYLOAD_OFFSET);
            Calculate_CRC(Frame, Frame.Length - FRAME_CRC_LEN).CopyTo(Frame, Frame.Length - FRAME_CRC_LEN);

            return Frame;
        }
        public bool Validate_frame(byte[] frame, int length)
        {
            // If frame is less the minimal lent of 6 directly exit.
            if (length < FRAME_MIN_LEN)
            {
                return false;
            }

            // If frame is less the minimal lent of 32 directly exit.
            if (length > FRAME_MAX_LEN)
            {
                return false;
            }

            // Check defined size.
            //if (frame[(int)FrameIndexes.LENGTH] != (length - FRAME_STATIC_FIELD_LENGTH))
            if (frame[(int)FrameIndexes.LENGTH] != (length))
            {
                return false;
            }

            // Check sentinel value.
            if (frame[(int)FrameIndexes.BEGIN] != FRAME_BEGIN_VALUE)
            {
                return false;
            }

            // Check request or response value.
            if ((frame[(int)FrameIndexes.REQUEST_RESPONSE] != (byte)REQUEST_RESPONSE.REQUEST) &&
                (frame[(int)FrameIndexes.REQUEST_RESPONSE] != (byte)REQUEST_RESPONSE.RESPONSE))
            {
                return false;
            }

            if (Validate_CRC(frame, length) == false)
            {
                return false;
            }

            return true;
        }

        public bool Validate_CRC(byte[] frame, int length)
        {

            byte[] CRC = new byte[FRAME_CRC_LEN] { 0, 0 };
            CRC = Calculate_CRC(frame, length - FRAME_CRC_LEN);

            // Check odd byte and even bytes.
            return (frame[length - 2] == CRC[0]) && (frame[length - 1] == CRC[1]);
        }

        public byte getOpCode_Fromframe(byte[] frame)
        {
            return frame[(int)FrameIndexes.OPERATION_CODE];
        }

        /// <summary>
        /// Подсчет контрольной суммы фрэйма
        /// </summary>
        protected byte[] Calculate_CRC(byte[] frame, int length)
        {
            byte[] crc = new byte[FRAME_CRC_LEN];

            for (int index = 0; index < length; index++)
            {
                // Odd
                if ((index % 2 == 0))
                {
                    // Sum all odd indexes.
                    crc[0] ^= frame[index]; //XOR
                }
                // Even
                else
                {
                    // Sum all even indexes.
                    crc[1] ^= frame[index]; //XOR
                }
            }

            return crc;

        }

        public FrameFormer(MCController aOwner)
        {
            this.owner = aOwner;
        }
        private void AddLog(string str)
        {
            if (owner == null) return;

            owner.Owner.AddLog(str);
        }
    }
}
