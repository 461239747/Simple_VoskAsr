这是一个简单的C#调用Vosk进行语音识别的案例,基于.NET4.7.2版本. 支持Unity调用.理论上也可以在.NET Core使用.
注意:Vosk仅支持64位程序进行初始化,请注意你的工程是否默认以64位进行运行或编译.
这里的案例主要以中文进行语音识别,如果需要其他语言可以在Vosk官网下载相应的模型.
语音录制使用的是NAudio库.你也可以使用其他方式来录制音频
AudioUnit引用了NAudio包.
VoskASR引用了Vosk包以及Newtonsoft.Json包.

This is a simple example of using Vosk for speech recognition in C#, based on the .NET 4.7.2 version. It supports calling from Unity and theoretically can be used in .NET Core as well.

Note: Vosk only supports initialization in 64-bit programs. Please ensure that your project is running or compiling in 64-bit mode.

This example mainly focuses on speech recognition in Chinese. If you need other languages, you can download the corresponding models from the Vosk official website.

NAudio library is used for audio recording in this example. You can also use other methods to record audio.

AudioUnit references the NAudio package.

VoskASR references the Vosk package and Newtonsoft.Json package.# Simple_VoskAsr
# Simple_VoskAsr
