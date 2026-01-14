const loginButton = document.getElementById("loginButton");
const phoneNumberInput = document.getElementById("Phone Number");
const resultDiv = document.getElementById("result");

function handleLoginResponse(response) {
    if (response.ok) {
        resultDiv.innerText = "Successful login";
    } else {
        resultDiv.innerText = "Login failed";
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