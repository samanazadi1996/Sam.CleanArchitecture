function myFunction() {
    const CreateElement = (tag, parent) => {
        const el = document.createElement(tag);
        parent.appendChild(el);
        return el;
    };
    const isDarkMode = () => document.documentElement.classList.contains('dark-mode');

    const body = document.body;
    if (!body) throw "Body element not found";

    // --- Floating Login Button ---
    const button = CreateElement("button", body);
    button.style.cssText = `
        position: fixed;
        bottom: 20px;
        right: 20px;
        background-color: #28a745;
        color: white;
        padding: 12px;
        border: none;
        border-radius: 50%;
        cursor: pointer;
        box-shadow: 0 4px 12px rgba(0,0,0,0.2);
        transition: all 0.3s ease;
        z-index: 1000;
    `;
    button.innerHTML = '<svg width="20" height="20"><use href="#unlocked" xlink:href="#unlocked"></use></svg>';
    button.onmouseover = () => button.style.transform = "scale(1.2)";
    button.onmouseout = () => button.style.transform = "scale(1)";

    button.onclick = () => {
        // --- Modal Container ---
        const dialog = CreateElement("div", body);
        dialog.style.cssText = `
            position: fixed;
            bottom: 20px;
            right: 20px;
            width: 320px;
            max-width: 90%;
            background-color: ${isDarkMode() ? "#1c2022" : "#fff"};
            border-radius: 12px;
            padding: 20px;
            box-shadow: 0 8px 24px rgba(0,0,0,0.25);
            border: 2px solid #28a745;
            z-index: 1001;
            font-family: Arial, sans-serif;
        `;

        // --- Modal Header ---
        const header = CreateElement("div", dialog);
        header.style.cssText = `
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 10px;
        `;
        const title = CreateElement("h3", header);
        title.innerText = "Login";
        title.style.margin = "0";
        title.style.color = isDarkMode() ? "#fff" : "#333";

        const closeBtn = CreateElement("button", header);
        closeBtn.innerText = "✖";
        closeBtn.style.cssText = `
            border: none;
            background: none;
            color: #dc3545;
            font-size: 18px;
            cursor: pointer;
            transition: transform 0.3s;
        `;
        closeBtn.onmouseover = () => closeBtn.style.transform = "rotate(180deg)";
        closeBtn.onmouseout = () => closeBtn.style.transform = "rotate(0deg)";
        closeBtn.onclick = () => body.removeChild(dialog);

        // --- Form ---
        const form = CreateElement("form", dialog);

        const createInputField = (labelText, type, value) => {
            const wrapper = CreateElement("div", form);
            wrapper.style.marginBottom = "12px";

            const label = CreateElement("label", wrapper);
            label.innerText = labelText;
            label.style.display = "block";
            label.style.marginBottom = "4px";
            label.style.fontWeight = "bold";
            label.style.color = isDarkMode() ? "#fff" : "#333";

            const input = CreateElement("input", wrapper);
            input.type = type;
            input.value = value || "";
            input.style.cssText = `
                width: 100%;
                padding: 8px 10px;
                border: 1px solid #ccc;
                border-radius: 6px;
                background-color: ${isDarkMode() ? "#1c2022" : "#fff"};
                color: ${isDarkMode() ? "#fff" : "#333"};
                font-size: 14px;
            `;
            return input;
        };

        const messageP = CreateElement("p", form);
        messageP.style.cssText = "color: red; text-align: center; margin-bottom: 10px; min-height: 18px;";

        const username = createInputField("Username", "text", localStorage.getItem("username"));
        const password = createInputField("Password", "password", localStorage.getItem("password"));

        // --- Show Password Toggle ---
        const toggleWrapper = CreateElement("div", form);
        toggleWrapper.style.marginBottom = "12px";
        const showPassBtn = CreateElement("button", toggleWrapper);
        showPassBtn.type = "button";
        showPassBtn.innerText = "Show Password";
        showPassBtn.style.cssText = `
            background: none;
            border: none;
            color: #007bff;
            cursor: pointer;
            font-size: 14px;
        `;
        showPassBtn.onclick = () => {
            password.type = password.type === "text" ? "password" : "text";
        };

        // --- Submit Button ---
        const submit = CreateElement("button", form);
        submit.type = "submit";
        submit.innerText = "Login";
        submit.style.cssText = `
            width: 100%;
            padding: 10px;
            background-color: #28a745;
            border: none;
            border-radius: 8px;
            color: white;
            font-size: 16px;
            cursor: pointer;
            transition: background 0.3s;
        `;
        submit.onmouseover = () => submit.style.backgroundColor = "#218838";
        submit.onmouseout = () => submit.style.backgroundColor = "#28a745";

        form.onsubmit = function (e) {
            e.preventDefault();
            fetch("/api/Account/Authenticate", {
                method: "POST",
                body: JSON.stringify({ userName: username.value, password: password.value }),
                headers: { "Content-type": "application/json; charset=UTF-8" },
            })
                .then(r => r.json())
                .then(json => {
                    if (json.success) {
                        localStorage.setItem("username", username.value);
                        localStorage.setItem("password", password.value);
                        window.ui.preauthorizeApiKey("Bearer", "bearer " + json.data.jwToken)
                        body.removeChild(dialog);
                    } else {
                        const errors = json.errors ? json.errors.map(e => e.description) : [];
                        messageP.innerHTML = errors.join("<br>") || "Login failed!";
                    }
                });
        };
    };
}

document.addEventListener("DOMContentLoaded", myFunction);
