using ScriptCs;
using ScriptCs.Contracts;

namespace ScriptCs.SpeakR.ScriptPack {

    public class SpeakRScriptPack: IScriptPack {

        private SpeakR speakr = new SpeakR();

        IScriptPackContext IScriptPack.GetContext() {
            return speakr;
        }

        void IScriptPack.Initialize(IScriptPackSession session) {
            session.AddReference("System.Speech");
            session.ImportNamespace("System.Speech.Synthesis");
        }

        void IScriptPack.Terminate() {
            speakr.Dispose();
        }

    }

}
