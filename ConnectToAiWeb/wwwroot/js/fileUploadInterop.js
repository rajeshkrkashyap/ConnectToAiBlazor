window.registerPasteEvent = (dotnetHelper) => {
     
    document.addEventListener("paste", (event) => {
        const clipboardData = event.clipboardData || window.clipboardData;
        const items = clipboardData.items;

        for (let i = 0; i < items.length; i++) {
            const item = items[i];

            if (item.kind === "file") {
                const file = item.getAsFile();
                const reader = new FileReader();
                reader.onloadend = function () {
                    var base64Data = reader.result;
                    try {
                        $('#imgData').attr('src', base64Data);
                        $('#mudText').css("display", "none");
                        $('#imgData').css("display", "block");
                        $('#imgData').css("height", "68px");
                        $('#imgData').css("margin-top", "-14px");
                        //window.GreetingHelpers.welcomeVisitor(base64Data);
                    } catch (e) {
                        console.log("Error message: " + e);

                    }
                };
                reader.readAsDataURL(file);
            }
        }
    });
};

function Upload() {
    var dataURL = $("#imgData").attr("src");
    console.log(dataURL);
    var byteString = atob(dataURL.split(',')[1]);
    var mimeType = dataURL.split(';')[0].slice(5); // Extract MIME type
    var arrayBuffer = new ArrayBuffer(byteString.length);
    var uint8Array = new Uint8Array(arrayBuffer);

    for (var i = 0; i < byteString.length; i++) {
        uint8Array[i] = byteString.charCodeAt(i);
    }
    console.log(mimeType);
    var blob = new Blob([arrayBuffer], { type: mimeType });
    var formData = new FormData();
    formData.append('file', blob, 'filename.png');
    let baseUrl = window.location.origin;
    const API_URL = baseUrl + "/home/Upload";
    //console.log(API_URL);
    $.ajax({
        type: 'POST',
        url: API_URL, //'@Url.Action("UploadAction", "Gpt")', // we are calling json method
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            console.log("File uploaded successfully:", response);
        },
        error: function (xhr, status, error) {
            console.error("Error uploading file:", error);
            // Handle upload error
        }
    });
}
 
window.getFileData = function () {
    return $('#imgData').attr("src");
};
window.openFileDialog = function () {
    var fileInput = document.querySelector('input[type=file]');
    fileInput.setAttribute('accept', 'image/*');
    fileInput.click();
};