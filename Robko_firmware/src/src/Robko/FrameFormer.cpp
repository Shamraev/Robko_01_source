#include "FrameFormer.h"

byte FrameFormer::getLength_Fromframe(byte *frame) {
     return frame[FrameIndexes::LENGTH];
}

byte FrameFormer::getOpCode_Fromframe(byte *frame)
{
    return frame[FrameIndexes::OPERATION_CODE];
}

void FrameFormer::getPayload(byte *frame, byte *payload, byte payloadLength)
{
    byte j = 0;
    for (byte i = FrameIndexes::PAYLOAD; i < payloadLength + FRAME_REQUEST_PAYLOAD_OFFSET; i++)
    {
        payload[j] = frame[i];
        j++;
    }
}
void FrameFormer::getAbsolute_Angles_q1q2q3_FromPayload(byte *payload, float *q)
{
    //size??
    // if ((sizeof(payload) != 12) || (sizeof(q) != 3)) return;//---

    q[0] = getFloatNumberFromByteArray(payload, 0);
    q[1] = getFloatNumberFromByteArray(payload, 4);
    q[2] = getFloatNumberFromByteArray(payload, 8);
}
float FrameFormer::getGripperAbsoluteDistance_FromPayload(byte *payload) {
    return getFloatNumberFromByteArray(payload, 0);
}

float FrameFormer::getFloatNumberFromByteArray(byte *byteArr, byte startByteIndex)
{
    byte byteArray[4];
    byte j = 0;
    for (byte i = startByteIndex; i < startByteIndex + 4; i++)
    {
        byteArray[j] = byteArr[i];
        j++;
    }
    return *((float *)(byteArray));
}

bool FrameFormer::Validate_frame(byte *frame, byte length)
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
    if (frame[FrameIndexes::LENGTH] != (length))
    {
        return false;
    }

    // Check sentinel value.
    if (frame[FrameIndexes::BEGIN] != FRAME_BEGIN_VALUE)
    {
        return false;
    }

    // Check request or response value.
    if ((frame[FrameIndexes::REQUEST_RESPONSE] != (byte)REQUEST_RESPONSE::REQUEST) &&
        (frame[FrameIndexes::REQUEST_RESPONSE] != (byte)REQUEST_RESPONSE::RESPONSE))
    {
        return false;
    }

    if (Validate_CRC(frame, length) == false)
    {
        return false;
    }

    return true;
}

bool FrameFormer::Validate_CRC(byte *frame, byte length)
{
    // byte CRC[FRAME_CRC_LEN] = {0, 0};
    Calculate_CRC(frame, length - FRAME_CRC_LEN, CRC_);

    // Check odd byte and even bytes.
    return (frame[length - 2] == CRC_[0]) && (frame[length - 1] == CRC_[1]);
}

void FrameFormer::Calculate_CRC(byte *frame, byte length, byte *crc)
{
    crc[0] = 0;
    crc[1] = 0;

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
}

void FrameFormer::DoResponseFrame_Done(byte *frame, byte length, byte opCode)
{
    // memset(frame, 0, sizeof(frame));//??--??
    frame[FrameIndexes::BEGIN] = FRAME_BEGIN_VALUE;
    frame[FrameIndexes::REQUEST_RESPONSE] = REQUEST_RESPONSE::RESPONSE;
    frame[FrameIndexes::LENGTH] = length;
    frame[FrameIndexes::OPERATION_CODE] = opCode;
    frame[FrameIndexes::STATUS_CODE] = STATUS_CODE::DONE;//!!done
    frame[FrameIndexes::PAYLOAD] = 0;
    frame[(FrameIndexes::PAYLOAD) + 1] = 0;

    Calculate_CRC(frame, length, CRC_);
    frame[length - 2] = CRC_[0];
    frame[length - 1] = CRC_[1];
}

void FrameFormer::DoResponseFrame_Error(byte *frame, byte length, byte opCode) {
    // memset(frame, 0, sizeof(frame));//??--??
    frame[FrameIndexes::BEGIN] = FRAME_BEGIN_VALUE;
    frame[FrameIndexes::REQUEST_RESPONSE] = REQUEST_RESPONSE::RESPONSE;
    frame[FrameIndexes::LENGTH] = length;
    frame[FrameIndexes::OPERATION_CODE] = opCode;
    frame[FrameIndexes::STATUS_CODE] = STATUS_CODE::ERROR;//!!error
    frame[FrameIndexes::PAYLOAD] = 0;
    frame[(FrameIndexes::PAYLOAD) + 1] = 0;

    Calculate_CRC(frame, length, CRC_);
    frame[length - 2] = CRC_[0];
    frame[length - 1] = CRC_[1];
    
}
