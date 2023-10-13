# Diff

## 설치

### 필수 구성 요소
1. [.NET 7 SDK](https://dotnet.microsoft.com/ko-kr/download/dotnet/7.0)

## 실행

### 실행 (오직 테스트 용도)

테스트용 `a.txt`, `b.txt` 파일을 생성 후 `Diff.Cli` 프로젝트의 매개변수로 실행

```bash
chmod +x ./test-run.sh
./test-run.sh
```

실행 결과는 아래와 같이 출력됩니다.
```diff
+ A
  H
  E
  L
  L
  O
+ -
```

### 실행 파일로 실행

저장소 루트 디렉토리로 이동합니다.

```bash
cd src/Diff.Cli
dotnet publish -c Release
cd bin/Release/net7.0

# 테스트용 a.txt, b.txt 파일 생성
# echo "HELLO" > a.txt
# echo "AHELLO-" > b.txt

./diff-cli a.txt b.txt
```

## 개발 과정

**개발 환경**
- MacOS
- Rider
- Git

### 1. 알고리즘 이해
알고리즘에 대한 이해가 없어, Diff 를 위한 알고리즘을 먼저 검색했습니다.  
이에 잘 설명된 내용을 검색하여 이해를 먼저 하고 진행하고자 했습니다. [[Link]](https://velog.io/@emplam27/%EC%95%8C%EA%B3%A0%EB%A6%AC%EC%A6%98-%EA%B7%B8%EB%A6%BC%EC%9C%BC%EB%A1%9C-%EC%95%8C%EC%95%84%EB%B3%B4%EB%8A%94-LCS-%EC%95%8C%EA%B3%A0%EB%A6%AC%EC%A6%98-Longest-Common-Substring%EC%99%80-Longest-Common-Subsequence)  
전반적으로 Diff 원리와 알고리즘에 대한 이해를 하고, 이를 바탕으로 구현을 하고자 하였습니다.  
물론 완벽하게 이해하지 못했지만, 시간이 부족하다고 판단하여 모두 완벽히 이해하지 못하고 구현을 진행 했습니다.

### 2. 알고리즘 구현
처음 알고리즘의 동작과 출력이 올바른지 검증하였습니다.  
[unused.DiffAlgorithm.cs](./src/Diff/unused.DiffAlgorithm.cs) 파일이 처음 알고리즘을 구현한 클래스 입니다.

- LCS 테이블을 생성하는 `Build` 메서드 구현
- LCS 테이블의 내용을 확인하는 `PrintText` 메서드 구현
- 출력 결과를 확인하는 [DiffPrintTests.DiffPrintTest](./src/Diff.Tests/DiffPrintTests.cs) 테스트 구현

### 3. 객체지향으로 개선
Diff 정보를 업데이트하고 계산하기 위해 몇 가지 구조체와 클래스로 분리하였습니다.

- [DiffChar.cs](./src/Diff/DiffChar.cs)  
  `char` 와 상응하는 `DiffChar` 구조체 입니다.
    - 문자(char) 자체를 나타내는 `DiffChar.Char` 속성
    - LCS 테이블에서 증감 값으로 사용할 `DiffChar.Value` 속성
    - Diff 결과를 나타나는 `DiffChar.Result` 속성
    - 그 외 문자의 비교를 위해 `GetHashCode`, `Equals` 메서드 구현를 재정의
- [DiffString.cs](./src/Diff/DiffString.cs)  
  DiffChar 배열을 갖는 Diff 문자열 클래스 입니다.  
  문자열을 인자로 자주 넘기는 것보다 힙에 할당하는 것이 더 효율적이라고 판단하여 구현하였습니다.  
  구현을 완료하진 못했지만 여러 라인의 Diff 결과를 갖는 `DiffString.Result` 속성이 있습니다. (사용 안함)
- [DiffSet.cs](./src/Diff/DiffSet.cs)  
  DiffString 의 원본과 사본의 정보를 갖고, Diff 를 실행하는 메서드를 갖는 클래스 입니다.

### 4. 인터페이스로 분리
다양한 알고리즘으로 구현할 수 있도록 인터페이스로 분리하였습니다.

- [IDiffSet.cs](./src/Diff/IDiffSet.cs)  
  `IDiffSet.Diff(IDiffAlgorithm)` 메서드를 갖는 인터페이스 입니다.
- [IDiffAlgorithm.cs](./src/Diff/IDiffAlgorithm.cs)  
  LCS 알고리즘을 구현한 클래스 입니다.

### 5. 테스트 코드 구현
LCS 테이블의 내용으로 알고리즘 결과를 검증하도록 간단하게 구현하였습니다.

[LcsAlorithmTests.cs](./src/Diff.Tests/LcsAlgorithmTests.cs) 테스트 코드에서 LCS 테이블을 문자열로 검증하도록 구현하였습니다.

### 6. CLI 프로그램으로 구현
CLI 프로그램으로 구현하기 위해 `Diff.Cli` 프로젝트를 생성하였습니다.
[App.Run](./src/Diff.Cli/App.cs) 메서드는 프로세스가 실행될 때 인자값으로 파일 이름 배열을 받습니다.

두 파일을 읽어 Diff 가 원본과 사본의 `DiffString` 을 생성하여 Diff 를 실행합니다.