#ifndef BusController_h
#define BusController_h

#include <Arduino.h>

#include "FrameFormer.h"

class BusController
{
public:
    void getFrame();
    void SendError();
    bool Validate_frame();
    void getOpCode();
    void getPayload();
    void getAbsolute_Angles_q1q2q3(float *q);
    void SendResponse_Done();

    byte opCode;

    void setSerial(Stream *streamObject);

private:
    Stream *_streamRef;

    FrameFormer frameFormer_;
    byte frame_[DATA_LENGTH];
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