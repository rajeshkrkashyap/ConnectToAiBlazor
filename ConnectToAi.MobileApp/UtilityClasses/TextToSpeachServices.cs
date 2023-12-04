using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToAi.MobileApp.UtilityClasses
{
    internal class TextToSpeachServices
    {

        public static async Task TextToSpeachSynthesizer(string textToSpeach, bool isStop)
        {
            //
            // For more samples please visit https://github.com/Azure-Samples/cognitive-services-speech-sdk 
            // 

            // Creates an instance of a speech config with specified subscription key and service region.
            string subscriptionKey = "13296b15a6a447ef8d1d904138a560d5";
            string subscriptionRegion = "centralus";

            var config = SpeechConfig.FromSubscription(subscriptionKey, subscriptionRegion);
            // Note: the voice setting will not overwrite the voice element in input SSML.
            config.SpeechSynthesisVoiceName = "en-US-JennyMultilingualNeural";

            //string text = "Hi, this is Jenny Multilingual";

            // use the default speaker as audio output.
            using (var synthesizer = new SpeechSynthesizer(config))
            {
                if (isStop)
                {
                    await synthesizer.StopSpeakingAsync();
                }
                using (var result = await synthesizer.SpeakTextAsync(textToSpeach))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"Speech synthesized for text [{textToSpeach}]");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                    }
                }
            }

        }
    }
}
