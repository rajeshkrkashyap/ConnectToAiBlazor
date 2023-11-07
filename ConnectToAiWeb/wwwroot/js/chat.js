$(document).ready(function () {
    //alert('Hi');
    const chatInput = document.querySelector("#chat-input");
    const chatContainer = document.querySelector("#chatContainer");
   // alert($(chatInput).val());

    const getChatResponse = async (incomingChatDiv, userPrompt, childInstructionId) => {
        const inputType = "text"; //chatInput.getAttribute("inputType");
        let baseUrl = window.location.origin;
        const API_URL = baseUrl + "/Index/GetChatResponse";  //'@Url.Action("AddTopicContent", "Question")';
        $.ajax({
            type: 'POST',
            url: API_URL,
            dataType: 'json',
            //data: {
            //    inputType: inputType,
            //    userPrompt: userPrompt,
            //    childInstructionId: childInstructionId
            //},
            success: function (result) {
                var altTexts = result.altTexts;
                //var id = generateGUID();
                var response = result.content;

                try {

                    var divElement = document.createElement("div");
                    // divElement.classList.add("copy-content");
                    divElement.style.float = "left";

                    if (inputType == "image") {
                        divElement.style.fontSize = "initial";
                    }

                    divElement.marginTop = "-35px";
                    //divElement.setAttribute("id", id);

                    var originalString = response;
                    if (originalString.charAt(0) === "'" || originalString.charAt(0) === ",") {
                        originalString = originalString.substr(1);
                    }

                    if (inputType == "image") {
                        divElement.innerHTML = originalString;
                        window.location.reload();
                    } else {
                        //divElement.innerHTML = marked(originalString);
                        divElement.innerHTML = originalString;
                        //typeText(divElement, marked(originalString), originalString.length);
                    }

                } catch (error) { // Add error class to the paragraph element and set error text
                    divElement.classList.add("error");
                    divElement.textContent = "Oops! Something went wrong while retrieving the response. Please try again.";
                }

                // Remove the typing animation, append the paragraph element and save the chats to local storage
                //incomingChatDiv.querySelector(".typing-animation").remove();
                incomingChatDiv.querySelector(".chat-details-inner").appendChild(divElement);

                let paragraphs = document.querySelectorAll('p');
                paragraphs.forEach(function (paragraph) {
                    if (paragraph.innerHTML.trim() === "") {
                        paragraph.remove();
                    }
                });

                if (altTexts != null) {
                    getImage(id);
                }
                localStorage.setItem("all-chats", chatContainer.innerHTML);
                chatContainer.scrollTo(0, chatContainer.scrollHeight);

                getSpeechFromAzure(originalString);
            },
            error: function (ex) {
                console.log(ex);
                alert('Failed to retrieve status.' + ex.responseText);
            }
        });

        // Send POST request to API, get response and set the reponse as paragraph element text
    }
    //getChatResponse(chatContainer,"Hi", "DA1EAEF6-9583-4B2C-B785-6C0603F13D34");

    
});