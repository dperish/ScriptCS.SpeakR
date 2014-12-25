using ScriptCs.Contracts;
using System;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Globalization;

namespace ScriptCs.SpeakR.ScriptPack {

    /// <summary>
    /// A script pack for ScriptCS that exposes the text-to-speech
    /// methods from the System.Speech library.
    /// </summary>
    public class SpeakR : IScriptPackContext, IDisposable {

        #region Private Members

        private SpeechSynthesizer _synth = new SpeechSynthesizer();

        private VoiceGender _gender = VoiceGender.Neutral;
        private VoiceAge _age = VoiceAge.NotSet;
        private int _voiceAlternate = 0;
        private System.Globalization.CultureInfo _culture = 
                    System.Globalization.CultureInfo.CurrentCulture;

        private void SelectVoice() {
            _synth.SelectVoiceByHints(_gender, _age, _voiceAlternate, _culture);
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Synchronously speaks the contents of a string.
        /// </summary>
        /// <param name="prompt">The text to speak.</param>
        public SpeakR Speak(string prompt) {
            _synth.Speak(prompt);
            return this;
        }

        /// <summary>
        /// Synchronously speaks and writes out the string contents
        /// to the console.
        /// </summary>
        /// <param name="prompt">The text to speak.</param>
        /// <returns></returns>
        public SpeakR SpeakWrite(string prompt) {
            Console.WriteLine(prompt);
            Speak(prompt);
            return this;
        }

        /// <summary>
        /// Configures the SpeechSynthesizer object to append output 
        /// to a file that contains Waveform format audio.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public SpeakR SetOutputToWaveFile(string path) {
            _synth.SetOutputToWaveFile(path);
            return this;
        }

        /// <summary>
        /// Returns all of the installed speech synthesis 
        /// (text-to-speech) voices.
        /// </summary>
        /// <returns>Returns a read-only collection of the 
        /// voices currently installed on the system.</returns>
        public Dictionary<string, VoiceInfo> GetInstalledVoices() {
            var installedVoices = new Dictionary<string, VoiceInfo>();
            foreach (var voice in _synth.GetInstalledVoices()) {
                installedVoices.Add(voice.VoiceInfo.Name, voice.VoiceInfo);
            }
            return installedVoices;
        }

        /// <summary>
        /// Selects a specific voice by name.
        /// </summary>
        /// <param name="name">The name of the voice to select.</param>
        public void SelectVoice(string name) {
            _synth.SelectVoice(name);
        }

        /// <summary>
        /// Selects a voice with specific characteristics.
        /// </summary>
        public void SelectVoiceByHints(VoiceGender gender, VoiceAge age, 
                int voiceAlternate, CultureInfo culture) {
            _gender = gender;
            _age = age;
            _voiceAlternate = voiceAlternate;
            _culture = culture;
            SelectVoice();
        }

        /// <summary>
        /// Sets the speaking rate of the SpeechSynthesizer object.
        /// </summary>
        /// <param name="rate">The speaking rate of the SpeechSynthesizer
        ///  object, from -10 through 10.</param>
        public SpeakR Rate(int rate) {
            _synth.Rate = rate;
            return this;
        }

        /// <summary>
        /// Sets the output volume of the SpeechSynthesizer object.
        /// </summary>
        /// <param name="volume">The volume of the SpeechSynthesizer, 
        /// from 0 through 100.</param>
        public SpeakR Volume(int volume) { 
            _synth.Volume = volume;
            return this;
        }

        /// <summary>
        /// Sets the gender of the SpeechSynthesizer object with a
        /// VoiceGender enumeration.
        /// </summary>
        public SpeakR Gender(VoiceGender gender) {
            _gender = gender;
            SelectVoice();
            return this;
        }

        /// <summary>
        /// Sets the gender of the SpeechSynthesizer object by 
        /// parsing a string into a VoiceGender enumeration.
        /// </summary>
        public SpeakR Gender(string gender) {
            return Gender(
                (VoiceGender)Enum.Parse(typeof(VoiceGender), gender, true));
        }

        /// <summary>
        /// Sets the age of the SpeechSynthesizer object with a
        /// VoiceAge enumeration.
        /// </summary>
        public SpeakR Age(VoiceAge age) {
            _age = age;
            SelectVoice();
            return this;
        }

        /// <summary>
        /// Sets the age of the SpeechSynthesizer object by 
        /// parsing a string into a VoiceAge enumeration.
        /// </summary>
        public SpeakR Age(string age) {
            return Age(
                (VoiceAge)Enum.Parse(typeof(VoiceAge), age, true));
        }

        /// <summary>
        /// Sets the position of the voice to select from the 
        /// installed voices. 
        /// </summary>
        public SpeakR VoiceAlternate(int voiceAlternate) {
            _voiceAlternate = voiceAlternate;
            SelectVoice();
            return this;
        }

        /// <summary>
        /// Sets the culture of the SpeechSynthesizer object to
        /// a System.Globalization.CultureInfo selection.
        /// </summary>
        public SpeakR Culture(CultureInfo culture) {
            _culture = culture;
            SelectVoice();
            return this;
        }

        /// <summary>
        /// Sets the culture of the SpeechSynthesizer object by
        /// parsing a string into a CultureInfo object.
        /// </summary>
        /// <param name="ietfLanguageTag">The name of a language as 
        /// specified by the RFC 4646 standard.</param>
        public SpeakR Culture(string ietfLanguageTag) {
            return Culture(
                CultureInfo.GetCultureInfoByIetfLanguageTag(
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