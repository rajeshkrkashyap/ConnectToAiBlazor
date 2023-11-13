window.loadStorageData = {
    loadDataFromLocalstorage: function (userId) {
        // Load saved chats and theme from local storage and apply/add on the page

        const defaultText = `<div class="default-text"><h1>Start a conversation</h1></div>`;

        var allChatContainer = document.getElementById("all-chat-container");
        //allChatContainer.innerHTML = localStorage.getItem("all-chats") || defaultText;
        var mainChatContainer = document.getElementById("main-chat-container");
        if (mainChatContainer != null)
            mainChatContainer.innerHTML = localStorage.getItem("all-chats" + userId);


        var libraryUrl = 'https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js';
        reloadLibrary(libraryUrl);
        //allChatContainer.scrollTo(0, allChatContainer.scrollHeight); // Scroll to bottom of the chat container
        //allChatContainer.scrollTop = allChatContainer.scrollHeight;
        let paragraphs = document.querySelectorAll('p');
        paragraphs.forEach(function (paragraph) {
            if (paragraph.innerHTML.trim() === "") {
                paragraph.remove();
            }
        });
        //adjustScroll();
        $(document).ready(function () {
            setTimeout(function () {
                document.getElementById('main-chat-container').scrollTop = document.getElementById('main-chat-container').scrollHeight;
                console.log(document.getElementById('main-chat-container').scrollTop);
            }, 500);
        });
    },
}
