document.getElementById("city").addEventListener("input", function () {
    var cityName = this.value.trim();

    if (cityName === "") {
        document.getElementById("city-list").innerHTML = "";
        return;
    }

    var requestData = {
        apiKey: "238ac926741f96a0b6563f3b5bd5e9d4",
        modelName: "Address",
        calledMethod: "searchSettlements",
        methodProperties: {
            CityName: cityName,
            Limit: "10",
            Page: "1"
        }
    };

    fetch('https://localhost:7201/searchSettlements', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestData)
    })
        .then(response => response.json())
        .then(data => {
            displaySearchResults(data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
});

function displaySearchResults(results) {
    var cityList = document.getElementById("city-list");
    cityList.innerHTML = "";

    if (results.data.length > 0 && results.data[0].Addresses.length > 0) {
        results.data[0].Addresses.forEach(function (result) {
            var cityName = result.MainDescription;

            var li = document.createElement("li");
            li.textContent = cityName;
            li.addEventListener("click", function () {
                document.getElementById("city").value = cityName;
                cityList.innerHTML = "";
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
    var autocomplete = document.querySelector(".autocomplete");
    var cityList = document.getElementById("city-list");

    if (event.target !== cityList && !cityList.contains(event.target)) {
        cityList.innerHTML = "";
    }
});
