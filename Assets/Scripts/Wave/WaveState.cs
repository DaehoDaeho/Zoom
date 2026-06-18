public enum WaveState
{
    Ready,  // 시작 전 상태.
    Spawning,   // 적을 생성하는 상태.
    WaitingClear,   // 생성 후 남은 적 처치를 기다리는 상태.
    Resting,    // 다음 웨이브로 가기 전 잠깐 쉬는 상태.
    Finished    // 모든 웨이브가 완료된 상태.
}