﻿
Office.onReady((info) => {
    if (info.host === Office.HostType.Word) {
        document.getElementById("pivot").addEventListener("click", (event) => {
            if (event.target.classList.contains("ms-Pivot-link")) {
                document.querySelectorAll(".ms-Pivot-link").forEach(link => link.classList.remove("is-selected"));
                event.target.classList.add("is-selected");

                document.querySelectorAll(".tab-content").forEach(tab => tab.style.display = "none");
                document.getElementById(event.target.getAttribute("data-content")).style.display = "block";
            }
        });

        document.querySelectorAll(".bookmark-link").forEach(link => {
            link.addEventListener("click", (event) => {
                const bookmarkName = event.target.getAttribute("data-bookmark");
                Word.run(async (context) => {
                    const bookmark = context.document.body.bookmarks.getItem(bookmarkName);
                    bookmark.select();
                    await context.sync();
                }).catch(error => {
                    console.log(error);
                });
            });
        });
    }
});


//let messageBanner;

//    // The initialize function must be run each time a new page is loaded.
//    Office.onReady(() => {
//        $(() => {
//            // Initialize he Office Fabric UI notification mechanism and hide it.
//            let element = document.querySelector('.MessageBanner');
//            messageBanner = new components.MessageBanner(element);
//            messageBanner.hideBanner();

//            // If not using Word 2016, use fallback logic.
//            if (!Office.context.requirements.isSetSupported('WordApi', '1.1')) {
//                $("#template-description").text("This sample displays the selected text.");
//                $('#button-text').text("Display!");
//                $('#button-desc').text("Display the selected text");
                
//                $('#highlight-button').on('click',displaySelectedText);
//                return;
//            }

//            $("#template-description").text("This sample highlights the longest word in the text you have selected in the document.");
//            $('#button-text').text("Highlight!");
//            $('#button-desc').text("Highlights the longest word.");
            
//            loadSampleData();

//            // Add a click event handler for the highlight button.
//            $('#highlight-button').on('click',hightlightLongestWord);
//        });
//    });

//    async function loadSampleData() {
//        try {
//            // Run a batch operation against the Word object model.
//            await Word.run(async (context) => {
//                // Create a proxy object for the document body.
//                let body = context.document.body;

//                // Queue a command to clear the contents of the body.
//                body.clear();
//                // Queue a command to insert text into the end of the Word document body.
//                body.insertText(
//                    "This is a sample text inserted in the document",
//                    Word.InsertLocation.end);

//                // Synchronize the document state by executing the queued commands, and return a promise to indicate task completion.
//                await context.sync();
//            })
//        } catch (error) {
//            errorHandler(error);
//        }
//    }

//    async function hightlightLongestWord() {
//        try {
//            await Word.run(async (context) => {
//            // Queue a command to get the current selection and then
//            // create a proxy range object with the results.
//            let range = context.document.getSelection();
            
//            // This variable will keep the search results for the longest word.
//            let searchResults;
            
//            // Queue a command to load the range selection result.
//            context.load(range, 'text');

//            // Synchronize the document state by executing the queued commands
//            // and return a promise to indicate task completion.
//            await context.sync()
//                // Get the longest word from the selection.
//                var words = range.text.split(/\s+/);
//                var longestWord = words.reduce(function (word1, word2) { return word1.length > word2.length ? word1 : word2; });

//                // Queue a search command.
//                searchResults = range.search(longestWord, { matchCase: true, matchWholeWord: true });

//                // Queue a command to load the font property of the results.
//                context.load(searchResults, 'font');

//                await context.sync();
//                // Queue a command to highlight the search results.
//                searchResults.items[0].font.highlightColor = '#FFFF00'; // Yellow
//                searchResults.items[0].font.bold = true;
//            })
//        } catch (error) {
//            errorHandler(error);
//        }
//    } 


//    function displaySelectedText() {
//        Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
//            function (result) {
//                if (result.status === Office.AsyncResultStatus.Succeeded) {
//                    showNotification('The selected text is:', '"' + result.value + '"');
//                } else {
//                    showNotification('Error:', result.error.message);
//                }
//            });
//    }

//    //$$(Helper function for treating errors, $loc_script_taskpane_home_js_comment34$)$$
//    function errorHandler(error) {
//        // $$(Always be sure to catch any accumulated errors that bubble up from the Word.run execution., $loc_script_taskpane_home_js_comment35$)$$
//        showNotification("Error:", error);
//        console.log("Error: " + error);
//        if (error instanceof OfficeExtension.Error) {
//            console.log("Debug info: " + JSON.stringify(error.debugInfo));
//        }
//    }

//    // Helper function for displaying notifications
//    function showNotification(header, content) {
//        $("#notification-header").text(header);
//        $("#notification-body").text(content);
//        messageBanner.showBanner();
//        messageBanner.toggleExpansion();
//    }
