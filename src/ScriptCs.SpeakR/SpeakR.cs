using ScriptCs.Contracts;
using System.Speech.Synthesis;
using System;
using System.Collections.Generic;

namespace ScriptCs.SpeakR.ScriptPack {

    public class SpeakR : IScriptPackContext, IDisposable {

        #region Private Members

        private SpeechSynthesizer _synth = new SpeechSynthesizer();

        private VoiceGender _gender = VoiceGender.NotSet;
        private VoiceAge _age = VoiceAge.NotSet;
        private int _voiceAlternate = 0;
        private System.Globalization.CultureInfo _culture = 
                    System.Globalization.CultureInfo.CurrentCulture;

        private void SelectVoice() {
            _synth.SelectVoiceByHints(_gender, _age, _voiceAlternate, _culture);
        }

        #endregion

        #region Public Members

        public void Speak(string prompt) {
            _synth.Speak(prompt);
        }

        public void SpeakWrite(string prompt) {
            Console.WriteLine(prompt);
            Speak(prompt);
        }

        public Dictionary<string, VoiceInfo> GetInstalledVoices() {
            var installedVoices = new Dictionary<string, VoiceInfo>();
            foreach (var voice in _synth.GetInstalledVoices()) {
                installedVoices.Add(voice.VoiceInfo.Name, voice.VoiceInfo);
            }
            return installedVoices;
        }

        public void SelectVoice(string name) {
            _synth.SelectVoice(name);
        }

        public void SelectVoice(VoiceGender gender, VoiceAge age, int voiceAlternate, 
                                System.Globalization.CultureInfo culture) {
            _gender = gender;
            _age = age;
            _voiceAlternate = voiceAlternate;
            _culture = culture;
            SelectVoice();
        }

        public SpeakR Rate(int rate) {
            _synth.Rate = rate;
            return this;
        }

        public SpeakR Volume(int volume) { 
            _synth.Volume = volume;
            return this;
        }

        public SpeakR Gender(VoiceGender gender) {
            _gender = gender;
            SelectVoice();
            return this;
        }

        public SpeakR Gender(string gender) {
            return Gender(
                (VoiceGender)Enum.Parse(typeof(VoiceGender), gender, true));
        }

        public SpeakR Age(VoiceAge age) {
            _age = age;
            SelectVoice();
            return this;
        }

        public SpeakR Age(string age) {
            return Age(
                (VoiceAge)Enum.Parse(typeof(VoiceAge), age, true));
        }

        public SpeakR VoiceAlternate(int voiceAlternate) {
            _voiceAlternate = voiceAlternate;
            SelectVoice();
            return this;
        }

        public SpeakR Culture(System.Globalization.CultureInfo culture) {
            _culture = culture;
            SelectVoice();
            return this;
        }

        public SpeakR Culture(string ietfLanguageTag) {
            return Culture(
                System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag(
                    ietfLanguageTag));
        }

        #endregion

        #region Disposer

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    _synth.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
        }

        #endregion

    }

}