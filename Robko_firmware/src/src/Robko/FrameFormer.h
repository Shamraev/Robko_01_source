#ifndef FrameFormer_h
#define FrameFormer_h

#include <Arduino.h>

namespace OPERATION_CODE
{
    enum Value
    {
        NONE = 0,
        MOVE_TO_ABSOLUTE_ANGLES_Q1Q2Q3 = 1,
        FIND_AND_GO_TO_ZEROS = 2,
        GRIPPER_GRIP = 3,                     //схват сжать до срабатывания датчика
        GRIPPER_GRIP_TO_ABSOLUTE_DISTANCE = 4 //схват сжать губки на определенное расстояние между ними
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

namespace FrameIndexes
{
    enum Value
    {
        BEGIN = 0,            //стартовый байт, 1 байт
        REQUEST_RESPONSE = 1, //байт запроса (ответа), 1 байт
        LENGTH = 2,           //байт длины payload, 1 байт;
        OPERATION_CODE = 3,   //байт кода операции, 1 байт;
        PAYLOAD = 4,          //байты полезной информации, байт;
        CRC = 5               //байты контрольной суммы, 2 байта.
    };
}

const byte FRAME_CRC_LEN = 2;
const byte FRAME_MIN_LEN = 6;
const byte FRAME_MAX_LEN = 32; //??
const byte DATA_LENGTH = 18;
const byte FRAME_LENGTH_WITHOUT_PAYLOAD = 6; //длина фрейма без полезной информации
// const byte FRAME_LENGTH_WITHOUT_CRC = DATA_LENGTH - FRAME_CRC_LEN;
const byte PAYLOAD_LENGTH = DATA_LENGTH - FRAME_LENGTH_WITHOUT_PAYLOAD;
const byte FRAME_BEGIN_VALUE = 1;
const byte FRAME_REQUEST_PAYLOAD_OFFSET = 4; //??
const byte RESPONSE_FRAME_LENGTH = 8;        //??6??

class FrameFormer
{
public:
    bool Validate_frame(byte *frame, byte length);
    byte getOpCode_Fromframe(byte *frame);
    void getPayload(byte *frame, byte *payload, byte payloadLength);
    void getAbsolute_Angles_q1q2q3_FromPayload(byte *payload, float *q);
    void DoResponseFrame_Done(byte *frame, byte length);
    void DoResponseFrame_Error(byte *frame, byte length);

protected:
    bool Validate_CRC(byte *frame, byte length);
    void Calculate_CRC(byte *frame, byte length, byte *crc);
    float getFloatNumberFromByteArray(byte *byteArr, byte startByteIndex);

private:
    byte CRC_[FRAME_CRC_LEN];//??--
};

#endif