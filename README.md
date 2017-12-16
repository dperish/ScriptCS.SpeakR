ScriptCs.SpeakR
==============================

A script pack for [ScriptCS](https://github.com/scriptcs) that exposes the text-to-speech methods from the System.Speech library.

Originally created to illustrate ScriptCS concepts in a 
[presentation](http://www.slideshare.net/DavidLeePerish/script-cs-for-business-and-pleasure) to the [Cleveland C#/VB.Net Special Interest Group](http://www.clevelanddotnet.info/).

## Installation

    C:\> scriptcs -install ScriptCS.SpeakR
    
## Documentation

Documentation for the SpeakR class can be found in the [Wiki](https://github.com/dperish/ScriptCS.SpeakR/wiki/SpeakR-Class-Documentation)

## Usage

#### Speak a string:

    var speakr = Require<SpeakR>();0    speakr.Speak("Hello world!");

#### Speak a string and also write the string to the Console:

    using(var speakr = Require<SpeakR>()) {
        speakr.SpeakWrite("Hello text & speech!");
    }
    
#### Selects a specific gender, culture, rate and speak the output to a wav file

    using(var speakr = Require<SpeakR>()) {
        speakr.Gender("female")
              .Culture("en-gb")
              .Rate(2)
              .SetOutputToWaveFile("helloWorld.wav")
              .Speak("Hello governer!");
    }

#### Selects a specific gender, culture, rate and read a text file from textfiles.com

    #r "System.Net.Http"

    const string url = "http://textfiles.com/etext/FICTION/alice11.txt";
    const string title = "ALICE'S ADVENTURES IN WONDERLAND";

    using(var speakr = Require<SpeakR>()) {
        speakr.Gender("female")
                .Culture("en-us")
                .Rate(0)
                .Speak(
                    title + " " + 
                    (new HttpClient())
                        .GetStringAsync(url)
                        .Result
                        .Split(new string[] { title}, StringSplitOptions.None)[1]
                        .Replace("\r\n", " "));
    }

## Future Enhancements

- Update the Slides to include the script authoring, github'ing and packaging steps
- Add functionality to get speech to text
- Add more methods from the base SpeechSynthesizer class, including:
  - SSML support
  - Async methods and delegates
  - Output to stream funtcionality

##License

The MIT License (MIT)

Copyright (c) 2013 David Lee Perish

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
