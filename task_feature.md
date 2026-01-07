# Task Feature Template (WPF MVVM)

이 문서는 범위, 규칙, 출력 형태를 고정하기 위한 작업 템플릿입니다. 요청 시 이 형식을 유지하여 작성합니다.

## 1. 범위 (Scope)
- 적용 대상: WPF (.NET 10.0) MVVM 프로젝트
- UI 구성: WebView2 사용
- 개발 환경: Visual Studio Code + Codex
- 디버깅/실행: Visual Studio

### 포함 (In-Scope)
- MVVM 패턴에 맞춘 View, ViewModel, Model, Service 작성/수정
- WebView2 초기화/사용/정리 로직
- 기존 프로젝트 구조 유지 및 필요한 최소 파일 추가
- 빌드/실행에 필요한 설정 유지

### 제외 (Out-of-Scope)
- 프로젝트 구조 대규모 재설계
- 프레임워크 전환 또는 대체 UI 기술 도입
- 불필요한 패키지 추가 및 의존성 변경

## 2. 고정 규칙 (Rules)

### 프로젝트 규칙
- WPF + MVVM 고정
- 타깃 프레임워크: .NET 10.0
- 기존 파일/폴더 구조를 유지

### UI 규칙
- WebView2 필수 사용
- WebView2 관련 초기화/해제는 ViewModel에서 처리하고, View에는 최소한의 XAML 및 바인딩만 사용

### 개발/실행 규칙
- 개발은 Visual Studio Code에서 Codex로 수행
- 디버깅 및 실행은 Visual Studio에서 수행

### 코드 스타일
- 단순하고 명확한 MVVM 구성
- View는 UI 요소와 바인딩만 포함
- ViewModel은 상태/명령/로직 담당
- Model은 순수 데이터 구조
- Service는 외부 연동/비즈니스 로직 담당

## 3. 출력 형태 (Output Format)
아래 형식을 반드시 따릅니다.

### 3.1 작업 요약
- 작업 목적:
- 변경 범위:
- 기대 결과:

### 3.2 수정/추가 파일 목록
- [파일 경로] : 변경 내용 요약

### 3.3 구현 상세
- View:
- ViewModel:
- Model:
- Service:
- WebView2 처리:

### 3.4 검증 방법
- Visual Studio에서 실행/디버깅 절차
- 예상 동작

## 4. 작업 요청 템플릿
아래 템플릿을 채워서 요청합니다.

### 4.1 기능 개요
- 기능명:
- 목적:
- 사용 시나리오:

### 4.2 요구 사항
- 필수 기능:
- 선택 기능:
- 제약 사항:

### 4.3 UI/UX
- 화면 구성:
- 사용 흐름:
- WebView2 표시 영역:

### 4.4 데이터 및 로직
- 사용 데이터:
- 상태 관리:
- 명령(Commands):

### 4.5 산출물
- 예상 변경 파일:
- 추가 리소스:

## 5. 예시 응답 형식

### 5.1 작업 요약
- 작업 목적: WebView2 기반 페이지 로딩 기능 추가
- 변경 범위: MainWindow.xaml, MainViewModel.cs
- 기대 결과: 앱 실행 시 지정 URL 표시

### 5.2 수정/추가 파일 목록
- MainWindow.xaml : WebView2 컨트롤 추가 및 바인딩
- ViewModels/MainViewModel.cs : URL 로딩 및 명령 구현

### 5.3 구현 상세
- View: WebView2 컨트롤 배치, Source 바인딩
- ViewModel: URL 상태 관리, 초기 로딩 명령
- Model: 필요 시 URL 설정 모델 추가
- Service: 외부 호출 없음
- WebView2 처리: 초기화 및 종료 시 Dispose

### 5.4 검증 방법
- Visual Studio에서 실행
- 앱 실행 시 WebView2에 URL 표시 확인
