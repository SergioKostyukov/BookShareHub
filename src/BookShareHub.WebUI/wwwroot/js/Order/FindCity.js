document.getElementById("city").addEventListener("input", function () {
    var cityName = this.value.trim();

    if (cityName === "") {
        document.getElementById("city-list").innerHTML = "";
        return;
    }

    var requestData = {
        apiKey: novaPoshtaApiKey,
        modelName: "Address",
        calledMethod: "searchSettlements",
        methodProperties: {
            CityName: cityName,
            Limit: "10",
            Page: "1"
        }
    };

    fetch('https://localhost:7201/NovaPoshtaRequest', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestData)
    })
        .then(response => response.json())
        .then(data => {
            console.log(data)
            displayCitySearchResults(data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
});

function displayCitySearchResults(results) {
    var cityList = document.getElementById("city-list");
    cityList.innerHTML = "";

    if (results.data.length > 0 && results.data[0].Addresses.length > 0) {
        results.data[0].Addresses.forEach(function (result) {
            var cityName = result.Present;
            var shortCityName = result.MainDescription;

            var li = document.createElement("li");
            li.textContent = cityName;
            li.addEventListener("click", function () {
                handleCityItemClick(cityName, shortCityName);
            });
            cityList.appendChild(li);
        });
    } else {
        var li = document.createElement("li");
        li.textContent = "No results found";
        cityList.appendChild(li);
    }
}

document.addEventListener("click", function (event) {
    var cityList = document.getElementById("city-list");

    if (event.target !== cityList && !cityList.contains(event.target)) {
        cityList.innerHTML = "";
    }
});

function handleCityItemClick(cityName, shortCityName) {
    document.getElementById("city").value = cityName;
    document.getElementById("cityTitle").value = shortCityName;
    var cityList = document.getElementById("city-list");
    cityList.innerHTML = "";
}
