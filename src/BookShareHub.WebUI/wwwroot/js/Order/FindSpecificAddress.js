document.getElementById("address").addEventListener("input", function () {
    var cityName = document.getElementById("cityTitle").value;
    var address = this.value.trim();

    var requestData = {
        apiKey: novaPoshtaApiKey,
        modelName: "Address",
        calledMethod: "getWarehouses",
        methodProperties: {
            FindByString: address,
            CityName: cityName,
            Page: "1",
            Limit: "10",
            Language: "UA"
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
            displayPointList(data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
});

function displayPointList(results) {
    var pointList = document.getElementById("point-list");
    pointList.innerHTML = "";

    if (results.data.length > 0) {
        results.data.forEach(function (point) {
            var pointName = point.Description;

            var li = document.createElement("li");
            li.textContent = pointName;
            li.addEventListener("click", function () {
                handlePointItemClick(pointName);
            });
            pointList.appendChild(li);
        });
    } else {
        var li = document.createElement("li");
        li.textContent = "No points found";
        pointList.appendChild(li);
    }
}

document.addEventListener("click", function (event) {
    var pointList = document.getElementById("point-list");

    if (event.target !== pointList && !pointList.contains(event.target)) {
        pointList.innerHTML = "";
    }
});

function handlePointItemClick(pointName) {
    document.getElementById("address").value = pointName;
    var pointList = document.getElementById("point-list");
    pointList.innerHTML = "";
}