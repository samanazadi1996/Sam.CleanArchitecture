function myFunction() {
    const CreateElement = (a, b, cls) => {
        var el = document.createElement(a);
        b.appendChild(el);
        if (cls != null) {
            var clsList = cls.split(" ");
            for (let index = 0; index < clsList.length; index++) {
                el.classList.add(clsList[index]);
            }
        }
        return el;
    };
    var profile = null;
    var body = document.getElementsByClassName("auth-wrapper")[0];
    if (!body) {
        throw ('New exception');
    }
    var button = CreateElement("button", body, "btn authorize unlocked");
    button.innerText = "Authenticate";

    button.onclick = function () {
        var dialogux = CreateElement("div", body, "dialog-ux");
        CreateElement("div", dialogux, "backdrop-ux");
        var modalux = CreateElement("div", dialogux, "modal-ux");
        var modaldialogux = CreateElement("div", modalux, "modal-dialog-ux");
        var modaluxinner = CreateElement("div", modaldialogux, "modal-ux-inner");
        var modaluxheader = CreateElement("div", modaluxinner, "modal-ux-header");
        var headertext = CreateElement("h3", modaluxheader, null);
        headertext.innerText = "Login";
        var closemodal = CreateElement("button", modaluxheader, "close-modal");
        closemodal.innerHTML = `<svg width="20" height="20"><use href="#close" xlink:href="#close"></use></svg>`;
        closemodal.onclick = function () {
            body.removeChild(dialogux);
        };
        var modaluxcontent = CreateElement("div", modaluxinner, "modal-ux-content");
        var authcontainer = CreateElement("div", modaluxcontent, "auth-container");
        var form = CreateElement("form", authcontainer, null);

        var messagewrapper = CreateElement("div", form, "wrapper");
        var messagep = CreateElement("h3", messagewrapper, null);
        messagep.innerText = "";
        messagep.setAttribute("style", "text-align: center;font-size: larger;color: red;");

        var usernamewrapper = CreateElement("div", form, "wrapper");
        var usernamelabel = CreateElement("label", usernamewrapper, null);
        usernamelabel.innerText = "UserName";
        var usernamesection = CreateElement("section", usernamewrapper, null);
        var username = CreateElement("input", usernamesection, null);
        username.value = localStorage.getItem("username");
        username.setAttribute("type", "text")

        var passwrapper = CreateElement("div", form, "wrapper");
        var passlabel = CreateElement("label", passwrapper, null);
        passlabel.innerText = "Password";
        var passsection = CreateElement("section", passwrapper, null);
        passsection.setAttribute("style", "display: flex;")

        var pass = CreateElement("input", passsection, null);
        var buttonshowpass = CreateElement("button", passsection, null);
        buttonshowpass.setAttribute("style", "margin: 5px;")

        buttonshowpass.innerHTML = `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" fill="#000000" version="1.1" id="Capa_1" width="24px" height="24x" viewBox="0 0 442.04 442.04" xml:space="preserve">  <g>  	<g>  		<path d="M221.02,341.304c-49.708,0-103.206-19.44-154.71-56.22C27.808,257.59,4.044,230.351,3.051,229.203    c-4.068-4.697-4.068-11.669,0-16.367c0.993-1.146,24.756-28.387,63.259-55.881c51.505-36.777,105.003-56.219,154.71-56.219    c49.708,0,103.207,19.441,154.71,56.219c38.502,27.494,62.266,54.734,63.259,55.881c4.068,4.697,4.068,11.669,0,16.367    c-0.993,1.146-24.756,28.387-63.259,55.881C324.227,321.863,270.729,341.304,221.02,341.304z M29.638,221.021    c9.61,9.799,27.747,27.03,51.694,44.071c32.83,23.361,83.714,51.212,139.688,51.212s106.859-27.851,139.688-51.212    c23.944-17.038,42.082-34.271,51.694-44.071c-9.609-9.799-27.747-27.03-51.694-44.071    c-32.829-23.362-83.714-51.212-139.688-51.212s-106.858,27.85-139.688,51.212C57.388,193.988,39.25,211.219,29.638,221.021z"/>  	</g>  	<g>  		<path d="M221.02,298.521c-42.734,0-77.5-34.767-77.5-77.5c0-42.733,34.766-77.5,77.5-77.5c18.794,0,36.924,6.814,51.048,19.188    c5.193,4.549,5.715,12.446,1.166,17.639c-4.549,5.193-12.447,5.714-17.639,1.166c-9.564-8.379-21.844-12.993-34.576-12.993    c-28.949,0-52.5,23.552-52.5,52.5s23.551,52.5,52.5,52.5c28.95,0,52.5-23.552,52.5-52.5c0-6.903,5.597-12.5,12.5-12.5    s12.5,5.597,12.5,12.5C298.521,263.754,263.754,298.521,221.02,298.521z"/>  	</g>  	<g>  		<path d="M221.02,246.021c-13.785,0-25-11.215-25-25s11.215-25,25-25c13.786,0,25,11.215,25,25S234.806,246.021,221.02,246.021z"/>  	</g>  </g>  </svg>    `;
        buttonshowpass.setAttribute("type", "button")
        buttonshowpass.onclick = function () {
            pass.setAttribute("type", pass.attributes["type"].value == "text" ? "password" : "text")
        };

        pass.value = localStorage.getItem("password");
        pass.setAttribute("type", "password")

        var authbtnwrapper = CreateElement("div", form, "auth-btn-wrapper");
        var submit = CreateElement("button", authbtnwrapper, "btn modal-btn auth authorize button");
        submit.innerText = "Login";
        form.onsubmit = function (e) {
            e.preventDefault();
            profile = null;
            fetch("/api/v1/Account/Authenticate", {
                method: "POST",
                body: JSON.stringify({
                    userName: username.value,
                    password: pass.value,
                }),
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                },
            })
                .then((response) => response.json())
                .then((json) => {
                    if (json.success) {
                        localStorage.setItem("username", username.value);
                        localStorage.setItem("password", pass.value);
                        profile = json.data;
                        body.removeChild(dialogux);
                    } else {
                        var errors = [];

                        if (json.errors)
                            for (let index = 0; index < json.errors.length; index++)
                                errors.push(json.errors[index].description);

                        messagep.innerHTML = errors.join("<br>");
                    }
                });
        };
    };

    const constantMock = window.fetch;
    window.fetch = function () {
        if (profile) {
            arguments[1].headers = {
                accept: "text/plain",
                "Content-Type": "application/json",
                Authorization: "Bearer " + profile.jwToken,
            };
        }
        return constantMock.apply(this, arguments);
    };
};

var intervalID = setInterval(function () {
    var body = document.getElementsByClassName("auth-wrapper")[0];
    if (body) {
        myFunction()
        clearInterval(intervalID);
    }
}, 1000);
