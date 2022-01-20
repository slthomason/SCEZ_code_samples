
async function postData(url, api_key, data) {
   
    // Default options are marked with *
    const response = await fetch(url, {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        headers: {
            'Content-Type': 'application/json',
            "x-api-key": api_key,
        },
        body: JSON.stringify(data) // body data type must match "Content-Type" header
    });
    console.log(response);
    return response.json(); // parses JSON response into native JavaScript objects
}


async function getData(url, api_key) {

    console.log('url : ' + url)
    // Default options are marked with *
    const response = await fetch(url, {
        method: 'GET', // *GET, POST, PUT, DELETE, etc.
        headers: {
            'Content-Type': 'application/json',
            "x-api-key": api_key,
        },
    });
    console.log(response);
    return response.json(); // parses JSON response into native JavaScript objects
}

async function postForm(url, api_key, file) {
  
    const form = new FormData();
    form.append("file", file);

    const response = fetch(url, {
        "method": "POST",
        "headers": {
            "x-api-key": api_key
        },
        body: form
    });
    console.log(response);
    return response.json();

}


