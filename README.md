# AiWPFDemo

WPF (.NET 10) MVVM sample로, 사용자가 선택한 Root Folder의 `WorkList.xml`을 읽고 연결된 PDF를 WebView2에 표시하는 읽기 전용 앱입니다. WorkItem마다 Image/Drawing/Model PDF 타입을 선택할 수 있습니다.

## Requirements
- Windows 10/11
- .NET 10 SDK (또는 Visual Studio의 .NET Desktop Workload)
- WebView2 Runtime (Evergreen)

## Build and Run (Visual Studio)
1. Visual Studio에서 `AiWPFDemo.slnx`를 엽니다.
2. NuGet Packages를 Restore합니다.
3. Build 후 Run합니다 (x64 권장).

## Development (VS Code + Codex)
- VS Code에서 소스 수정
- Visual Studio에서 Debug/Run

## Usage
1. `Browse...`를 눌러 Root Folder를 선택합니다.
2. Folder List에서 Subfolder를 선택합니다.
3. WorkItem List에서 항목을 선택합니다 (Name만 표시).
4. PDF Type: Image / Drawing / Model 중 하나를 선택합니다.
5. 선택한 PDF가 WebView2 Area에 표시됩니다.

## Data Format
각 Subfolder에는 아래 구조의 `WorkList.xml`이 있어야 합니다.

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

## Notes
- `Image`, `Drawing`, `Model` 값은 동일 폴더 내 PDF 파일명으로 처리됩니다.
- Save/Modify 기능은 제공하지 않습니다 (Read-only).
