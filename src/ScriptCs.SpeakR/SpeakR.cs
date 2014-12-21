using ScriptCs.Contracts;
using System.Speech.Synthesis;
using System;

namespace ScriptCs.SpeakR.ScriptPack {

    public class SpeakR : IScriptPackContext, IDisposable {

        private SpeechSynthesizer _synth = new SpeechSynthesizer();      

        public void Speak(string text) {
            _synth.Speak(text);
        }

        public void SpeakWrite(string text) {
            Console.WriteLine(text);
            Speak(text);
        }

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