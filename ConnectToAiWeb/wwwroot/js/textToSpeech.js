window.aiSpeech = {
    // Define synthesizer as a global variable
    //synthesizeSpeech: function (textContent) {
    //    var resourceKey = "13296b15a6a447ef8d1d904138a560d5";
    //    var resourceRegion = "centralus";
    //    var speechConfig = SpeechSDK.SpeechConfig.fromSubscription(resourceKey, resourceRegion);
    //    var synthesizer = new SpeechSDK.SpeechSynthesizer(speechConfig);
    //    var audioConfig = SpeechSDK.AudioConfig.fromDefaultSpeakerOutput();
    //    synthesizer.audioConfig = audioConfig;
    //    var result = synthesizer.speakTextAsync(
    //        //SpeechSDK.SpeechSynthesisVoiceName.enUSJennyMultilingualNeural, // Voice name
    //        //"Hello, this is a test.", // Text to synthesize
    //        textContent,
    //        //SpeechSDK.AudioOutputFormat["Audio16Khz32KBitRateMonoMp3"], // Output format
    //        function (result) {
    //            if (result.reason === SpeechSDK.ResultReason.SynthesizingAudioCompleted) {
    //                //synthesizer.pause();
    //                //var audioData = result.audioData;
    //                //var blob = new Blob([audioData], { type: "audio/mpeg" }); // Assuming MP3 format
    //                //var url = URL.createObjectURL(blob);

    //                //// Assuming you have an audio element with id="audioElement" in your HTML
    //                ////var audioPlayer = document.createElement("audio");
    //                ////audioPlayer.id = audioElemebntId;

    //                //var audioPlayer = document.getElementById(audioElemebntId);
    //                //console.log(audioPlayer);
    //                //if (audioPlayer) {
    //                //    audioPlayer.src = url;
    //                //    audioPlayer.load(); // Load the audio
    //                //    //audioElement.mute();
    //                //    //audioElement.play(); // Play the audio
    //                //    console.log(url);
    //                //    //var messageElement = document.getElementById(messageId);
    //                //    //if (messageElement) {
    //                //    //    messageElement.appendChild(audioPlayer);
    //                //    //    console.log("audio element is appened");
    //                //    //}
    //                //}
    //            } else {
    //                console.error("Speech synthesis failed: " + result.errorDetails);
    //            }
    //        },
    //        function (err) {
    //            console.error("Error during speech synthesis: " + err);
    //        }
    //    );
    //},

    synthesizeSpeech: function (synthesizer, textContent) {

        var audioConfig = SpeechSDK.AudioConfig.fromDefaultSpeakerOutput();
        synthesizer.audioConfig = audioConfig;

        var isPaused = false; // Flag to track if the speech is paused

        var result = synthesizer.speakTextAsync(
            textContent,
            function (result) {
                if (result.reason === SpeechSDK.ResultReason.SynthesizingAudioCompleted) {
                    // Speech synthesis completed
                } else {
                    console.error("Speech synthesis failed: " + result.errorDetails);
                }
            },
            function (err) {
                console.error("Error during speech synthesis: " + err);
            }
        );

        // Pause function
        function pauseSpeech() {
            if (isPaused) return; // Already paused
            synthesizer.pause();
            isPaused = true;
        }

        // Resume function
        function resumeSpeech() {
            if (!isPaused) return; // Not paused
            synthesizer.resume();
            isPaused = false;
        }

        return {
            pause: pauseSpeech,
            resume: resumeSpeech
        };
    },
    getSpeechFromAzure: function (textContent) {
        try {
            var resourceKey = "13296b15a6a447ef8d1d904138a560d5";
            var resourceRegion = "centralus";
            var speechConfig = SpeechSDK.SpeechConfig.fromSubscription(resourceKey, resourceRegion);
            var synthesizer = new SpeechSDK.SpeechSynthesizer(speechConfig);
            var speech = this.synthesizeSpeech(synthesizer, textContent);
        }
        catch (ex) {
            console.log("error 1 " + ex);
        }
    },


    speechRecognizer: function (speechRecognizer, inputElement) {

        speechRecognizer.recognizing = (s, e) => {
            console.log(`RECOGNIZING: Text=${e.result.text}`);
        };

        speechRecognizer.recognized = (s, e) => {
            if (e.result.reason == SpeechSDK.ResultReason.RecognizedSpeech) {

                inputElement.value += e.result.text + " ";

                console.log(`RECOGNIZED: Text=${e.result.text}`);
            } else if (e.result.reason == SpeechSDK.ResultReason.NoMatch) {
                console.log("NOMATCH: Speech could not be recognized.");
            }
        };

        speechRecognizer.canceled = (s, e) => {
            console.log(`CANCELED: Reason=${e.reason}`);

            if (e.reason == SpeechSDK.CancellationReason.Error) {
                console.log(`"CANCELED: ErrorCode=${e.errorCode}`);
                console.log(`"CANCELED: ErrorDetails=${e.errorDetails}`);
                console.log("CANCELED: Did you set the speech resource key and region values?");
            }

            speechRecognizer.stopContinuousRecognitionAsync();
        };

        speechRecognizer.sessionStopped = (s, e) => {
            console.log("\n    Session stopped event.");
            speechRecognizer.stopContinuousRecognitionAsync();
        };

        console.log("Start");
        speechRecognizer.startContinuousRecognitionAsync();
    },
    getTextFromSpeechAzure: function (inputElement, isStop) {
        try {
            console.log("isStop: " + isStop);
            let speechRecognizer;
            if (isStop == false) {
                var resourceKey = "13296b15a6a447ef8d1d904138a560d5";
                var resourceRegion = "centralus";
                var speechConfig = SpeechSDK.SpeechConfig.fromSubscription(resourceKey, resourceRegion);
                const audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
                speechRecognizer = new SpeechSDK.SpeechRecognizer(speechConfig, audioConfig);
                this.speechRecognizer(speechRecognizer, inputElement, isStop);
            } else {

                // To stop recognition, call the following line at some point:
                console.log("Stop");
                speechRecognizer.stopContinuousRecognitionAsync();
            }
        }
        catch (ex) {
            console.log("error 1 " + ex);
        }
    },
    testAlert: function (content) {
        alert(content);
    },
    loadSpeech: function () {
        if (!!window.SpeechSDK) {
            SpeechSDK = window.SpeechSDK;
            console.log("Loaded");
        }
    },
}
aiSpeech.loadSpeech();