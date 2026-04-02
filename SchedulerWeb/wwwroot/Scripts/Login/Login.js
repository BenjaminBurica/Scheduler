const loginButton = document.getElementById("loginButton");
const phoneNumberInput = document.getElementById("Phone Number");
const resultDiv = document.getElementById("result");

async function handleLoginResponse(response) {
    if (response.ok) {
        const data = await response.json();
        if (data.success){
            resultDiv.innerText = "Succesful login";
        } else{
            resultDiv.innerText = "Login failed";
        } 
    } else{
        resultDiv.innerText = "Error";
    }
}


async function handleLoginClick() {
    const phoneNumber = phoneNumberInput.value;
    const request = { phoneNumber: phoneNumber };
    try {
        await fetch("/api/login/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(request)
        }).then(handleLoginResponse);
    } catch {
        resultDiv.innerText = "Login failed";
    }
}

if (loginButton && resultDiv) {
    loginButton.addEventListener("click", handleLoginClick);
}