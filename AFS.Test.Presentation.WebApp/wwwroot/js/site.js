function getBaseUrl() {
    return $("#baseUrl").val();
}
function replaceSpacesWithPercent20(inputString) {
    return inputString.replace(/ /g, "%20");
}
function callCreateTranslationEndpoint(text, translation, translated) {
    const postData = {
        Translation: translation,
        Text: text,
        Translated: translated
    };

    const jsonData = JSON.stringify(postData);

    $.ajax({
        url: `https://localhost:7188/api/translations`,
        method: 'POST',
        contentType: 'application/json',
        data: jsonData,
        success: function() {
            $('#toast').toast('show');
        },
        error: function(xhr, status, error) {
            // error message
            console.log(error);
        }
    });
}
function callTranslateApi(){
    
    const type = $("#type").val().trim();
    const text = $("#text").val().trim();
    const button = $("#submitBtn");
    
    if (type === "" || text === "") {
        alert("No field should be empty")
        return;
    }
    
    button.text("Calling Translation API ... Be Patient");
    button.prop("disabled", true);

    $.ajax({
        url: `https://api.funtranslations.com/translate/${type}?text=${replaceSpacesWithPercent20(text)}`,
        method: 'GET',
        dataType: 'json',
        success: function(data) {
            
            $('#resultSuccess').html(data.contents.translated);
            $("#resultSuccess").removeAttr("hidden");
            
            // call endpoint to add translation call to the database
            callCreateTranslationEndpoint(data.contents.text, data.contents.translation, data.contents.translated)
        },
        error: function(xhr, status, error) {
            $('#resultFailed').html('Error: ' + error);
            $("#resultFailed").removeAttr("hidden");
        }
    });

    $("#type").val(" ");
    $("#text").val(" ");
    button.text("Translate Text!");
    button.prop("disabled", false);
}
function filterTranslationCallsList() {
    const type = $("#filterByType").val().trim();
    const keyword = $("#filterByKeyword").val().trim();
    const date = $("#filterByDate").val();
    
    window.location.href = getBaseUrl() + `?date=${date}&keyword=${keyword}&type=${type}`;
}

$(document).ready(function() {

    $('.collapse').collapse()
   
    $("#myForm").submit(function(event) {
        event.preventDefault();
        
        callTranslateApi();
    });
    
    $("#filterForm").submit(function (event) {
        event.preventDefault();
        
        filterTranslationCallsList()
    })

    window.addEventListener('popstate', function(event) {
        // Reload the window when the URL changes
        window.location.reload();
    });

    $(window).scroll(function() {
        if ($(this).scrollTop() > 30) {
            $('#toTopBtn').fadeIn();
        } else {
            $('#toTopBtn').fadeOut();
        }
    });

    $('#toTopBtn').click(function() {
        $("html, body").animate({
            scrollTop: 0
        }, 1000);
        return false;
    });
});