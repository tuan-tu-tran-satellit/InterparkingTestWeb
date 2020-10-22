// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    let apiKey = window.com_satellit_api_key;
    //callback that receives the search term
    //calls the azure search api
    //returns the results to the response callback
    function getParkings(request, response) {
        $.get({
            url: "https://atlas.microsoft.com/search/fuzzy/json",
            dataType: "json",
            data: {
                "subscription-key": apiKey,
                "api-version": "1.0",
                "query": request.term,
                "countrySet": "BE",
                "brandSet": "Interparking",
                "typeahead": true,
            },
            success: function (data) {
                var results = $.map(data.results, match => {
                    return {
                        "label": match.poi.name + " - " + match.address.freeformAddress,
                        position: match.position,
                    }
                })
                response(results);
            }
        });
    }
    function setupAutoComplete(idPrefix) {
        $(`#${idPrefix}Search`).autocomplete({
            source: getParkings,
            select: function (event, ui) {
                $(`#${idPrefix}Latitude`).val(ui.item.position.lat);
                $(`#${idPrefix}Longitude`).val(ui.item.position.lon);
            }
        });
    }
    setupAutoComplete("start");
    setupAutoComplete("end");
})