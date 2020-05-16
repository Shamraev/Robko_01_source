#ifndef FrameFormer_h
#define FrameFormer_h

#include <Arduino.h>

namespace FrameIndexes
{
    enum Value
    {
        BEGIN,            //стартовый байт, 1 байт
        REQUEST_RESPONSE, //байт запроса (ответа), 1 байт
        LENGTH,           //байт длины payload, 1 байт;
        OPERATION_CODE,   //байт кода операции, 1 байт;
        STATUS_CODE,      //байт кода статуса, 1 байт;
        PAYLOAD,          //байты полезной информации, байт;
        CRC               //байты контрольной суммы, 2 байта.
    };
}
namespace REQUEST_RESPONSE
{
    enum Value
    {
        REQUEST = 0,
        RESPONSE = 1
    };
}
namespace OPERATION_CODE
{
    enum Value
    {
        NONE,
        MOVE_TO_ABSOLUTE_ANGLES_Q1Q2Q3,
        FIND_AND_GO_TO_ZEROS,
        GRIPPER_GRIP,                     //схват сжать до срабатывания датчика
        GRIPPER_UNGRIP,                   //схват разжать до срабатывания датчика + еще немного ??
        GRIPPER_OPEN_TO_ABSOLUTE_DISTANCE //схват сжать губки на определенное расстояние между ними
    };
}
namespace STATUS_CODE
{
    enum Value
    {
        NONE,
        ERROR,
        DONE
    };
}

const byte FRAME_CRC_LEN = 2;
const byte FRAME_MIN_LEN = 7;
const byte FRAME_MAX_LEN = 32; //??
const byte DATA_LENGTH = 19;
const byte FRAME_LENGTH_WITHOUT_PAYLOAD = 7; //длина фрейма без полезной информации
                                             // const byte FRAME_LENGTH_WITHOUT_CRC = DATA_LENGTH - FRAME_CRC_LEN;
const byte PAYLOAD_LENGTH = DATA_LENGTH - FRAME_LENGTH_WITHOUT_PAYLOAD;
const byte FRAME_BEGIN_VALUE = 1;
const byte FRAME_REQUEST_PAYLOAD_OFFSET = (byte)FrameIndexes::PAYLOAD; //??
const byte RESPONSE_FRAME_LENGTH = 9; //??6??

class FrameFormer
{
public:
    bool Validate_frame(byte *frame, byte length);
    byte getLength_Fromframe(byte *frame); //---
    byte getOpCode_Fromframe(byte *frame);
    void getPayload(byte *frame, byte *payload, byte payloadLength);
    void getAbsolute_Angles_q1q2q3_FromPayload(byte *payload, float *q);
    float getGripperAbsoluteDistance_FromPayload(byte *payload);
    void DoResponseFrame_Done(byte *frame, byte length, byte opCode);
    void DoResponseFrame_Error(byte *frame, byte length, byte opCode);

protected:
    bool Validate_CRC(byte *frame, byte length);
    void Calculate_CRC(byte *frame, byte length, byte *crc);
    float getFloatNumberFromByteArray(byte *byteArr, byte startByteIndex);

private:
    byte CRC_[FRAME_CRC_LEN]; //??--
};

#endif