var profiles = {};

profiles.init = function () {

};

profiles.removeSearch = function () {
    $.removeCookie('search', { path: '/' });

    window.location = "/Profiles";
}

profiles.save = function (createNew) {

    var profile = {
        ProfileID: $("#profileID").val(), //Reading text box values using Jquery   
        Surname: $("#surname").val(),
        Name: $("#name").val(),
        FatherName: $("#fatherName").val(),

        MotherSurName: $("#motherSurName").val(),
        Brothers: $("#brothers").val(),
        Sisters: $("#sisters").val(),
        Gender: $("#genderMale").hasClass('active') ? 1 : 0,
        DateOfBirth: $("#dateOfBirth").val(),
        Height: $("#height").val(),
        Colour: $("#colour").val(),
        PlaceOfBirth: $("#placeOfBirth").val(),
        Raasi: $("#raasi").val(),
        BirthStar: $("#birthStar").val(),
        Education: $("#education").val(),
        Occupation: $("#occupation").val(),
        AnnualIncome: $("#annualIncome").val(),
        Requirement: $("#requirement").val(),
        Address: $("#address").val(),
        Phone: $("#phone").val(),
        Mobile: $("#mobile").val(),
        Email: $("#email").val()

    };

  

    var urlString = "/Profiles/Save?";

    urlString = urlString + '&createNew=' + createNew;

    $.cookie('profile', JSON.stringify(profile), { path: '/' });

    $.ajax(
        {
            type: "POST", //HTTP POST Method  
            url: urlString, // Controller/View   
           
            cache: false,
            success: function (response) {
                $.removeCookie('profile', { path: '/' });
                debugger;
                alert(response);
                if (response.indexOf('Error') === -1) {
                    window.location = "/Profiles";
                }

            },
            error: function (xhr, textStatus, errorThrown) {
                // TODO: Show error
            }


        });

    return false;
};