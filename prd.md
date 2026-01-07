# PRD - XML PDF Viewer (WPF MVVM)

## 1. 목적
사용자가 선택한 루트 폴더 내 하위 폴더 목록을 읽어 리스트로 표시하고, 각 폴더의 `WorkList.xml`을 기반으로 PDF 파일을 WebView2에 표시하는 읽기 전용 WPF MVVM 프로그램을 정의한다.

## 2. 범위

### 포함
- 사용자 선택 루트 폴더 하위 폴더 목록 로드 및 리스트 표시
- 각 하위 폴더의 `WorkList.xml` 파일 읽기
- 리스트 선택 시 `WorkList.xml`의 내용에 따라 PDF 파일을 WebView2에 표시
- XML 구조 파싱 및 표시용 ViewModel 구성

### 제외
- 저장 기능
- 초기화 기능
- 데이터베이스 연동
- 네비게이션(다중 페이지 전환)
- 검증 규칙

## 3. 사용자 시나리오
1. 앱 실행
2. 사용자가 루트 폴더를 선택
3. 선택된 루트 폴더에서 하위 폴더 목록을 읽어 리스트로 표시
4. 사용자가 리스트 항목(폴더)을 선택
5. 선택된 폴더의 `WorkList.xml`을 파싱
6. XML 내용에 따라 PDF 파일을 WebView2에 표시

## 4. 요구사항

### 4.1 기능 요구사항
- FR-01: 사용자가 루트 폴더를 선택한다.
- FR-02: 루트 폴더의 하위 폴더명을 리스트로 표시한다.
- FR-03: 리스트 항목(폴더) 선택 시 해당 폴더의 `WorkList.xml`을 읽는다.
- FR-04: `WorkList.xml` 내 `WorkItem` 목록을 표시한다.
- FR-05: 리스트에는 `WorkItem.Name`만 표시한다.
- FR-06: `WorkItem`의 `Image`, `Drawing`, `Model` 값은 PDF 파일명으로 간주한다.
- FR-07: `Image`, `Drawing`, `Model` 중 선택한 항목을 PDF로 표시한다.
- FR-08: 사용자가 `WorkItem`을 선택하면 선택된 항목의 PDF 파일을 WebView2에 표시한다.
- FR-09: `Issue`는 여러 줄 값이며 `;` 구분자로 분리하여 표시한다.
- FR-10: 데이터는 읽기 전용이며 저장/초기화 기능은 제공하지 않는다.

## 4.3 구현 현황 (체크리스트)
- [x] 사용자 시나리오 2: 루트 폴더 선택
- [x] 사용자 시나리오 3: 하위 폴더 목록 표시
- [x] 사용자 시나리오 4: 폴더 선택
- [x] 사용자 시나리오 5: WorkList.xml 파싱
- [x] 사용자 시나리오 6: PDF 표시
- [x] FR-01 루트 폴더 선택
- [x] FR-02 하위 폴더 리스트 표시
- [x] FR-03 WorkList.xml 읽기
- [x] FR-04 WorkItem 목록 표시
- [x] FR-05 WorkItem.Name만 표시
- [x] FR-06 Image/Drawing/Model을 PDF 파일명으로 처리
- [x] FR-07 선택한 유형 PDF 표시
- [x] FR-08 WorkItem 선택 시 PDF 표시
- [ ] FR-09 Issue 분리 표시
- [x] FR-10 읽기 전용

### 4.2 비기능 요구사항
- NFR-01: WPF + MVVM 패턴을 따른다.
- NFR-02: .NET 10.0 타깃을 유지한다.
- NFR-03: UI 표시에는 WebView2를 사용한다.
- NFR-04: 디버깅/실행은 Visual Studio에서 수행한다.

## 5. 데이터

### 5.1 XML 구조
- 루트: `<Products>`
- 노드: `<Product name="" folder="" customer="" rev="">`
- 하위: `<WorkItem>` 리스트
- `Issue`: `;` 구분자로 분리되는 다중 항목

```xml
<Products>
  <Product name="" folder="" customer="" rev="">
    <WorkItem>
      <Code></Code>
      <Name></Name>
      <Image></Image>
      <Drawing></Drawing>
      <Model></Model>
      <Issue></Issue>
    </WorkItem>
  </Product>
</Products>
```

### 5.2 경로 규칙
- 루트 폴더: 사용자 선택 경로
- XML 경로: `[하위 폴더]\WorkList.xml`
- PDF 경로: `[하위 폴더]\[Image|Drawing|Model]`

## 6. UI/UX

### 6.1 화면 목적
- 리스트에 연결된 PDF 파일을 WebView2에 표시

### 6.2 화면 구성
- 상단: 루트 폴더 선택 영역
- 좌측: 하위 폴더 리스트
- 중단: 선택된 폴더의 WorkItem 리스트(`Name`만 표시)
- 중단 하단: PDF 유형 선택 라디오(Image/Drawing/Model)
- 우측: WebView2 PDF 표시 영역
- 좌측 리스트 영역 : 우측 WebView2 영역 가로 비율 = 3 : 7

### 6.3 상호작용
- 루트 폴더 선택 -> 하위 폴더 리스트 로드
- 하위 폴더 선택 -> WorkItem 목록 로드
- PDF 유형 라디오 선택 -> 표시 대상 유형 설정
- WorkItem 선택 -> 선택된 유형의 PDF 표시
- Issue 목록은 줄 단위로 표시

### 6.4 와이어프레임
```
┌──────────────────────────────────────────────────────────────────────────┐
│ 루트폴더: [경로 표시] [찾아보기...]                                      │
├──────────────────────────────┬───────────────────────────────────────────┤
│ 리스트 영역 (3)              │ WebView2 영역 (7)                         │
│ ┌───────────────┐            │                                           │
│ │ 폴더 리스트   │            │                                           │
│ │ [Folder 1]    │            │                                           │
│ │ [Folder 2]    │            │                                           │
│ │ [Folder 3]    │            │                                           │
│ └───────────────┘            │                                           │
│ ┌───────────────┐            │                                           │
│ │ WorkItem 목록 │            │                                           │
│ │ [Name A]      │            │                                           │
│ │ [Name B]      │            │                                           │
│ │ [Name C]      │            │                                           │
│ └───────────────┘            │                                           │
│ PDF 유형: ( ) Image ( ) Drawing ( ) Model                                 │
└──────────────────────────────┴───────────────────────────────────────────┘
```

#### 6.4.1 레이아웃 규칙
- 전체 그리드: 상단 1행(루트폴더 선택), 하단 1행(본문)
- 본문 컬럼 비율: 리스트 영역 : WebView2 영역 = 3 : 7
- 리스트 영역은 세로 스택(폴더 리스트, WorkItem 리스트, 라디오 그룹)

#### 6.4.2 표시 규칙
- WorkItem 리스트는 `Name`만 표시
- 선택된 PDF 유형(Image/Drawing/Model)에 따라 WebView2 표시 대상 결정
- WebView2 영역은 시각적으로 구분되도록 테두리와 안내 문구를 표시한다

#### 6.4.3 컨트롤 구성
- 루트폴더 선택: TextBox(경로 표시, 읽기 전용) + Button(찾아보기)
- 폴더 리스트: ListBox
- WorkItem 리스트: ListBox
- PDF 유형: RadioButton 3개(Image/Drawing/Model)
- PDF 표시: WebView2

### 6.5 XAML 레이아웃 설계(초안)
```xml
<Grid>
  <Grid.RowDefinitions>
    <RowDefinition Height="Auto"/>
    <RowDefinition Height="*"/>
  </Grid.RowDefinitions>
  <Grid.ColumnDefinitions>
    <ColumnDefinition Width="3*"/>
    <ColumnDefinition Width="7*"/>
  </Grid.ColumnDefinitions>

  <!-- 루트 폴더 선택 -->
  <DockPanel Grid.Row="0" Grid.ColumnSpan="2" Margin="8">
    <TextBlock Text="루트폴더:" VerticalAlignment="Center" Margin="0,0,8,0"/>
    <TextBox Text="{Binding RootFolderPath}" IsReadOnly="True" Width="480" Margin="0,0,8,0"/>
    <Button Content="찾아보기..." Command="{Binding BrowseRootFolderCommand}" Width="120"/>
  </DockPanel>

  <!-- 리스트 영역 (3) -->
  <Grid Grid.Row="1" Grid.Column="0" Margin="8,0,8,8">
    <Grid.RowDefinitions>
      <RowDefinition Height="*"/>
      <RowDefinition Height="*"/>
      <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>

    <GroupBox Header="폴더 리스트" Grid.Row="0" Margin="0,0,0,8">
      <ListBox ItemsSource="{Binding FolderItems}"
               SelectedItem="{Binding SelectedFolder}"/>
    </GroupBox>

    <GroupBox Header="WorkItem 목록" Grid.Row="1" Margin="0,0,0,8">
      <ListBox ItemsSource="{Binding WorkItems}"
               SelectedItem="{Binding SelectedWorkItem}"
               DisplayMemberPath="Name"/>
    </GroupBox>

    <StackPanel Grid.Row="2" Orientation="Horizontal">
      <TextBlock Text="PDF 유형:" VerticalAlignment="Center" Margin="0,0,8,0"/>
      <RadioButton Content="Image" IsChecked="{Binding IsImageSelected}"/>
      <RadioButton Content="Drawing" IsChecked="{Binding IsDrawingSelected}" Margin="8,0,0,0"/>
      <RadioButton Content="Model" IsChecked="{Binding IsModelSelected}" Margin="8,0,0,0"/>
    </StackPanel>
  </Grid>

  <!-- WebView2 영역 (7) -->
  <Border Grid.Row="1" Grid.Column="1" Margin="0,0,8,8"
          BorderBrush="#C7CBD1" BorderThickness="1" CornerRadius="6"
          Background="#F7F8FA">
    <Grid Margin="8">
      <TextBlock Text="PDF 표시 영역"
                 HorizontalAlignment="Center"
                 VerticalAlignment="Center"
                 Foreground="#6B7280"/>
      <wv2:WebView2 Source="{Binding PdfSource}"/>
    </Grid>
  </Border>
</Grid>
```

### 6.6 MVVM 바인딩 항목 정의
- RootFolderPath: string, 루트 폴더 경로 표시
- BrowseRootFolderCommand: ICommand, 폴더 선택 다이얼로그 실행
- FolderItems: IEnumerable<FolderItem>, 루트 하위 폴더 목록
- SelectedFolder: FolderItem, 선택된 폴더
- WorkItems: IEnumerable<WorkItem>, 선택된 폴더의 WorkItem 목록
- SelectedWorkItem: WorkItem, 선택된 WorkItem
- IsImageSelected: bool, PDF 유형 Image 선택 여부
- IsDrawingSelected: bool, PDF 유형 Drawing 선택 여부
- IsModelSelected: bool, PDF 유형 Model 선택 여부
- PdfSource: Uri 또는 string, WebView2 표시 대상 PDF 경로

## 7. 검증 및 테스트
- 검증 규칙: 없음
- 테스트: 수동 확인 (폴더/WorkList.xml/PDF 파일 로딩 확인)

## 8. 가정 및 제약
- 모든 하위 폴더에는 `WorkList.xml`이 존재한다고 가정한다.
- PDF 파일은 해당 폴더 내에 존재한다고 가정한다.
- 네트워크 접근은 필요하지 않다.
- 파일 읽기 오류 처리(권한/누락)는 최소한의 예외 처리로 대응한다.

## 9. 산출물
- `prd.md`
- 구현 시 필요 파일(예: View, ViewModel, Model, Service)
