# 오디오 출처

- 제작자: Kenney
- 팩: [RPG Audio](https://kenney.nl/assets/rpg-audio)
- 라이선스: [CC0 1.0](https://creativecommons.org/publicdomain/zero/1.0/)
- 원본 라이선스: `Kenney-RPG-Audio-License.txt`
- 다운로드: 2026-09-20

| 프로젝트 파일 | 원본 파일 | 연결된 동작 | 변환 |
| --- | --- | --- | --- |
| sfx_swing.wav | Audio/knifeSlice.ogg | 일반 스윙 | PCM 16-bit WAV, gain 0.65 |
| sfx_swing_strong.wav | Audio/knifeSlice2.ogg | 강타 스윙 | PCM 16-bit WAV, gain 0.80 |
| sfx_mode.wav | Audio/metalClick.ogg | 우클릭 모드 전환 | PCM 16-bit WAV, gain 0.50 |

원본의 48 kHz / stereo를 유지했습니다. 런타임에서는 기존 G2AudioSound로 재생하며 별도 decoder 패키지가 필요하지 않습니다.

## 추가 오디오 (2026-09-20)

아래 소스는 모두 원본 게시 페이지에서 CC0를 확인했습니다. 변환 결과는 PCM 16-bit WAV이며 runtime 의존성은 추가하지 않았습니다.

| 프로젝트 파일 | 작품 / 제작자 / 출처 | 원본 | 편집 |
| --- | --- | --- | --- |
| amb_cave.wav | [Loopable Dungeon Ambience / JaggedStone](https://opengameart.org/node/29778) | dungeon_ambient_1_0.ogg | 앞 24초, 경계 1초 crossfade → 23초 loop, peak 0.24, 48 kHz stereo |
| sfx_rock_break.wav | [75 CC0 breaking / falling / hit sfx / rubberduck](https://opengameart.org/content/75-cc0-breaking-falling-hit-sfx) | bfh1_rock_breaking_01.ogg | peak 0.65, 48 kHz stereo |
| sfx_bounce.wav | 위 rubberduck 팩 | bfh1_rock_hit_01.ogg | peak 0.40, 48 kHz stereo |
| sfx_breath.wav | [Breathing Tired / mikeask](https://opengameart.org/content/breathing-tired) | breathing tired.wav | 전체 3.17초, peak 0.55, 44.1 kHz stereo |
| sfx_oxygen.wav | 위 mikeask 호흡 녹음 | breathing tired.wav | 앞 1.5초를 회복용 짧은 호흡으로 사용, peak 0.38, 44.1 kHz stereo |
| sfx_cheer.wav | [Well Done / qubodup](https://opengameart.org/content/well-done) | Well Done CCBY3.ogg | 전체 약 4초 박수, peak 0.55, 44.1 kHz mono |

효과음에는 시작/끝 15 ms fade를 적용했습니다. peak는 편집 후 최대 절대 진폭(0~1)입니다.
`Well Done`은 파일명에 CCBY3가 남아 있지만 제작자가 게시 페이지에서 2024-10-05에 CC0로 변경했다고 명시했습니다.
회복음은 산소통 자체를 녹음한 소리가 아니라 호흡 녹음을 편집한 대체 효과음입니다.
배경은 음악 대신 동굴의 낮은 바람/물방울 ambience를 사용하며 별도 타이틀 음악은 추가하지 않았습니다.

원본 다운로드:

- [동굴](https://opengameart.org/sites/default/files/dungeon_ambient_1_0.ogg)
- [돌 효과음 팩](https://opengameart.org/sites/default/files/sfx_breaking_and_falling.zip)
- [호흡](https://opengameart.org/sites/default/files/breathing%20tired.wav)
- [박수](https://opengameart.org/sites/default/files/Well%20Done%20CCBY3.ogg)
