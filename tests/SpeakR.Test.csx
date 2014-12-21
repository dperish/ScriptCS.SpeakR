var speakr = Require<SpeakR>();

if (Env.ScriptArgs.Count > 0) {
    Env.ScriptArgs.ToList().ForEach(arg => {
        speakr.SpeakWrite(arg);
    });
}
else {
    speakr.Speak("This is spoken but not written to the console.");
    speakr.SpeakWrite("This is spoken and written to the console.");
}
