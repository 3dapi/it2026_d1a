# 리소스 출처 및 사용 조건

확인·다운로드: 2026-09-19~20. 게임 실행 시 모든 파일은 로컬에서 읽습니다.

## 음악 — CC0

| 프로젝트 파일 | 작품 / 제작자 | 사용 위치 | 원본 페이지 | 다운로드 |
|---|---|---|---|---|
| `Assets/Audio/dream.mp3` | Vampire's Piano / TAD | 시작·탐색·대화, 반복 재생 | https://opengameart.org/content/vampires-piano | https://opengameart.org/sites/default/files/vampires_piano_6.mp3 |
| `Assets/Audio/farewell.mp3` | Regret - Short Emotional Piano / Wolfgang_ | 정상 엔딩·아침 장면 | https://opengameart.org/content/regret-short-emotional-piano | https://opengameart.org/sites/default/files/piano_nostalgia_0.mp3 |

두 원본 페이지 모두 라이선스가 CC0로 표시되어 있는 것을 확인했습니다. MP3 파일 자체는 수정하지 않았고 게임에서 재생 음량과 전환만 조절합니다.

## 효과음 — Kenney Interface Sounds 1.0 / CC0

원본 페이지: https://kenney.nl/assets/interface-sounds  
원본 ZIP: https://kenney.nl/media/pages/assets/interface-sounds/fa43c1dd4d-1677589452/kenney_interface-sounds.zip  
제작·배포: Kenney (https://kenney.nl)  
동봉된 원본 라이선스: `Assets/Audio/Kenney-License.txt`

| 프로젝트 파일 | 원본 파일 | 사용 위치 | 수정 |
|---|---|---|---|
| `click.wav` | `click_003.ogg` | 메뉴와 대화 진행 | PCM 16-bit WAV 변환 |
| `memory.wav` | `confirmation_002.ogg` | 기억 획득 | PCM 16-bit WAV 변환 |
| `door.wav` | `open_002.ogg` | 문 이동·지름길 개방·재도전 | PCM 16-bit WAV 변환 |
| `alert.wav` | `error_002.ogg` | 몬스터 발견 예고 | PCM 16-bit WAV 변환 |
| `fail.wav` | `error_006.ogg` | 잡힘·기억 부족 결말 | PCM 16-bit WAV 변환 |

라이선스 링크: https://creativecommons.org/publicdomain/zero/1.0/

## 새로 합성한 소리

`Assets/Audio/pulse.wav`: 이번 프로젝트용으로 생성한 65Hz 저음 두 번의 감쇠 파형입니다. 추격 시 박동 신호로 사용합니다. 외부 녹음이나 음악 샘플은 포함하지 않았습니다. 생성 원리는 `DESIGN.md`에 설명되어 있습니다.

## 그래픽

다음 이미지는 사용자가 이 대화에 제공한 자료입니다. 별도의 외부 무료 그래픽으로 설명하거나 CC0 라이선스를 부여하지 않았습니다.

| 프로젝트 파일 | 제공 파일 ID | 사용 위치 |
|---|---|---|
| `Art/title.png` | cdea287e-06da-4ece-87a4-4aae0f154cb6 | 시작 화면 |
| `Art/portrait.png` | ba1b38b2-2409-4ebd-8788-64b4e5a470bb | 탐색 왼쪽 패널 |
| `Art/world.png` | b5134a21-41cd-468e-bb95-44ffc879ca8a | 탐색 배경 분위기 |
| `Art/player.png` | 55411760-4821-45fa-9904-bb97838e8ac8 | 플레이어·엔딩 |
| `Art/her.png` | 752dfb76-35ac-4e58-a665-bf5cd1c7c858 | 마지막 구역·엔딩 |
| `Art/monster.png` | e270f1b2-baed-44fa-ae76-b0b865d464a3 | 후회괴물 |
| `Art/ending.png` | 4a8b0f1a-b1f9-4f39-b63a-6ce938ae1ba6 | 정상 엔딩 |

원본 이미지는 파일 수준에서 수정하지 않고, 게임 실행 중 화면에 맞게 크기와 투명도를 조절합니다. 실제 맵의 바닥·기둥·빛·꽃잎·UI·아침 화면은 System.Drawing 코드로 그립니다. 튜닉의 이미지·음원·맵 데이터를 사용하지 않았습니다.
