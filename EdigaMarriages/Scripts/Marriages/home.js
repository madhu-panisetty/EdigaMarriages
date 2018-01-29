var home = {};

home.init = function () {
    
};

home.autoCompleteSurname = function () {

    var urlString = "/Home/GetSurnames";
    
    //$.cookie('profile', JSON.stringify(profile), { path: '/' });

    $.ajax(
        {
            type: "POST", //HTTP POST Method  
            url: urlString, // Controller/View   
            cache: false,
            success: function (response) {
                
                var availableTags = response;

                $("#surname").autocomplete({
                    source: availableTags
                });
            },
            error: function (xhr, textStatus, errorThrown) {
                // TODO: Show error
            }


        });

  
}


home.serachByID = function () {

    if ($("#profileID").val() === '') {
        alert("Profile ID is required");
        return;
    }

    var urlString = "/Profiles/Details?";

    urlString = urlString + 'profileID=' + $("#profileID").val();

    window.location = urlString;
    
};


home.searchBySurname = function (gender) {

    if ($("#surname").val() === '') {
        alert("Surname is required");
        return;
    }

    var search = {
        Surname: $("#surname").val(),
        Gender: gender
    }
    $.cookie('search', JSON.stringify(search));

    var urlString = "/Profiles/Index?";
   
    window.location = urlString;

};