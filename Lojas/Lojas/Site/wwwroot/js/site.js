
window.onload = function () {
    // Verificar se o usuário está logado ao carregar a página
    checkJwtCookie();
};

// Função para verificar o JWT no cookie
function checkJwtCookie() {
    fetch('/Home/CheckJwtCookie', {
        method: 'GET',
        credentials: 'same-origin' // Enviar cookies junto com a requisição
    })
        .then(response => response.json())
        .then(data => {
            if (data.isAuthenticated) {
                // Se o token JWT foi encontrado, atualizar a interface
                updateLoginStatus(true);
            } else {
                // Se o token JWT não foi encontrado, manter como "Entrar"
                updateLoginStatus(false);
            }
        })
        .catch(error => {
            console.error('Erro ao verificar o cookie:', error);
        });
}

// Função para atualizar o botão de login para "Logado"
function updateLoginStatus(isLoggedIn) {
    const loginLink = document.querySelector(".login-link");
    const registerLink = document.querySelector(".register-link");
    const separator = document.querySelector(".auth-links span");
    const authLinks = document.querySelector(".auth-links");

    if (isLoggedIn) {
        // Se estiver logado, mudar para "Logado" e adicionar o ícone de usuário
        loginLink.textContent = "Logado";  // Mudar o texto
        loginLink.setAttribute("href", "#");

        // Criar e adicionar o ícone de usuário
        const userIcon = document.createElement("i");
        userIcon.classList.add("fas", "fa-user", "mg-5");

        // Inserir o ícone antes do texto
        loginLink.prepend(userIcon);

        // Esconder "Cadastrar" e o separador "|"
        if (registerLink) {
            registerLink.style.display = "none";
        }

        if (separator) {
            separator.style.display = "none";
        }

        // Adicionar o dropdown com "Perfil" e "Sair"
        const dropdown = document.createElement("div");
        dropdown.classList.add("dropdown");
        const dropdownToggle = document.createElement("div");
        //dropdownToggle.classList.add("btn", "btn-secondary", "dropdown-toggle");
        dropdownToggle.classList.add("div_logado");
        dropdownToggle.setAttribute("role", "button");
        dropdownToggle.setAttribute("data-bs-toggle", "dropdown");
        dropdownToggle.setAttribute("aria-expanded", "false");
        //dropdownToggle.textContent = "Conta"; // Texto que será mostrado no botão de dropdown
        dropdownToggle.innerHTML = '<i class="fas fa-user-circle"></i> Conta'; // Ícone de conta + texto


        // Criar o menu do dropdown
        const dropdownMenu = document.createElement("ul");
        dropdownMenu.classList.add("dropdown-menu");
        dropdownMenu.setAttribute("aria-labelledby", "dropdownMenuButton");
        // Opção "Perfil" com ícone Font Awesome
        const profileOption = document.createElement("li");
        const profileLink = document.createElement("a");
        profileLink.classList.add("dropdown-item");
        profileLink.setAttribute("href", "/Perfil");  // Redirecionar para a página de perfil
        profileLink.innerHTML = '<i class="fas fa-user"></i> Perfil';  // Ícone de usuário + texto
        profileOption.appendChild(profileLink);

        // Opção "Sair" com ícone Font Awesome
        const logoutOption = document.createElement("li");
        const logoutLink = document.createElement("a");
        logoutLink.classList.add("dropdown-item");
        logoutLink.setAttribute("href", "/conta/Logout");  // Redirecionar para a página de logout
        logoutLink.innerHTML = '<i class="fas fa-sign-out-alt"></i> Sair';  // Ícone de logout + texto
        logoutOption.appendChild(logoutLink);

        // Adicionar as opções ao menu
        dropdownMenu.appendChild(profileOption);
        dropdownMenu.appendChild(logoutOption);

        // Adicionar o toggle e o menu ao dropdown
        dropdown.appendChild(dropdownToggle);
        dropdown.appendChild(dropdownMenu);

        // Substituir o link de login pelo dropdown
        authLinks.innerHTML = "";
        authLinks.appendChild(dropdown);
    } else {
        // Se não estiver logado, manter como "Entrar"
        loginLink.textContent = "Entrar";
        loginLink.setAttribute("href", "/Conta/Login");

        // Mostrar "Cadastrar" e o separador "|"
        if (registerLink) {
            registerLink.style.display = "inline";
        }

        if (separator) {
            separator.style.display = "inline";
        }

        // Remover o dropdown
        authLinks.innerHTML = "";
        authLinks.appendChild(registerLink);
        authLinks.appendChild(separator);
        authLinks.appendChild(loginLink);
    }
}
