# Fighter Jet

**과목명:** 객체지향프로그래밍  
**작성자:** 권민기  
**학번:** 26031005  
**작성일:** 2026-08-31  
**최종 업데이트:** 2026-09-16  
**과제명:** 4주차 — 게임 완성 및 최종 제출

---

## 게임 소개

세로 비율(9:16) 종스크롤 슈팅 생존 게임입니다.  
플레이어는 전투기를 조종해 위에서 내려오는 몬스터를 피하거나 자동으로 발사되는 총알로 처치하며 5분간 생존하는 것이 목표입니다.

## 조작 방법

| 키 | 동작 |
|---|---|
| ↑ / ↓ / ← / → | 전투기 이동 |
| Space (시작 화면) | 게임 시작 |
| Space (결과 화면) | 게임 재시작 |
| (자동) | 총알은 별도 입력 없이 자동 발사 |

## 게임 규칙

- HP 100으로 시작, 몬스터와 충돌 시 HP 10 감소 (충돌 후 1초 무적)
- 몬스터는 총알 3방에 처치되며, 처치 시 100점 획득
- HP가 0이 되면 게임 오버(패배)
- 3분간 생존하면 생존 성공(승리)
- 게임 종료 시 최종 점수가 결과 화면에 표시됨

## 폴더 구조

```
Fighter_jet/
├─ README.md
├─ doc/
│  ├─ GameDesign.md      # 게임 기획서 (클래스 구성 포함)
│  └─ ResourceList.md    # 리소스 출처 및 라이선스 문서
├─ resource/
│  ├─ texture/           # 이미지 리소스 (플레이어/몬스터/총알)
│  └─ sound/             # 효과음 리소스
├─ glc2d/                # 게임 프레임워크 (3dapi 작성)
└─ (C# 소스 코드: GameMain.cs, Player.cs, Bullet.cs, Monster.cs, GameGlobal.cs, AppMain.cs 등)
```

## 사용 기술

- C# / .NET 9 (Windows Forms)
- glc2d (Direct2D 기반 2D 렌더링 프레임워크)
- Vortice.Direct2D1 / DirectWrite / XAudio2

## 실행 방법

1. Visual Studio에서 `Fighter_jet.sln` 열기
2. `F5`로 빌드 및 실행
3. 스페이스바를 눌러 게임 시작

## 리소스 출처

이미지 및 사운드 리소스의 출처와 라이선스는 [`doc/ResourceList.md`](doc/ResourceList.md)에 정리되어 있습니다.
