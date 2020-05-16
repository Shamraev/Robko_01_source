#ifndef BusController_h
#define BusController_h

#include <Arduino.h>

#include "FrameFormer.h"

class BusController
{
public:
    void getFrame();
    void SendResponse_Error();
    bool Validate_frame();
    void parse_frame();
    void getAbsolute_Angles_q1q2q3(float *q);
    float getGripperAbsoluteDistance();
    void SendResponse_Done();

    byte opCode;

    void setSerial(Stream *streamObject);

private:
    Stream *_streamRef;

    FrameFormer frameFormer_;
    static const byte length_ = DATA_LENGTH;//??
    byte frame_[length_];
    byte payload_[PAYLOAD_LENGTH];

    byte frameResponse[RESPONSE_FRAME_LENGTH];

    // byte* frame_P = frame;
    // byte* length_P = &length;
    // byte* payload_P = payload;
    // byte* payloadLength_P = &payloadLength;
public:
    BusController();
    ~BusController();
};

#endif