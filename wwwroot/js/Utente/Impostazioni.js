$(document).ready(function () {
  const emailRegex = new RegExp(
    "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
  );
  const passwordRegex = new RegExp(
    "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})",
  );

  function showError(element, message) {
    $(element).text(message).removeClass("opacity-0").addClass("opacity-100");
  }

  function hideError(element) {
    $(element).removeClass("opacity-100").addClass("opacity-0");
  }

  function validateField(value, regex, errorElement, errorMessage) {
    if (!regex.test(value)) {
      showError(errorElement, errorMessage);
      return false;
    }
    hideError(errorElement);
    return true;
  }

  function validatePasswordFields() {
    const password = $("#password").val();
    const confirmPassword = $("#confirmPassword").val();
    const isPwdValid = validateField(
      password,
      passwordRegex,
      "#pwdError",
      "La password deve contenere almeno 8 caratteri, di cui almeno una lettera maiuscola, una lettera minuscola, un numero e un carattere speciale.",
    );
    const isConfirmPwdValid = password === confirmPassword;

    if (!isPwdValid) {
      $("#password")
        .addClass("outline-gray-300/20 focus:outline-orange-500")
        .removeClass("outline-success-500 focus:outline-success-500");
    } else {
      $("#password")
        .addClass("outline-success-500 focus:outline-success-500")
        .removeClass("outline-gray-300/20 focus:outline-orange-500");
    }

    if (!isConfirmPwdValid) {
      showError("#confirmPwdError", "Le password non coincidono.");
      $("#confirmPassword")
        .addClass("outline-gray-300/20 focus:outline-orange-500")
        .removeClass("outline-success-500 focus:outline-success-500");
    } else {
      hideError("#confirmPwdError");
      $("#confirmPassword")
        .addClass("outline-success-500 focus:outline-success-500")
        .removeClass("outline-gray-300/20 focus:outline-orange-500");
    }

    return isPwdValid && isConfirmPwdValid;
  }

  function validaForm() {
    const nome = $("#nome").val().trim();
    const cognome = $("#cognome").val().trim();
    const email = $("#email").val().trim();

    const isNomeValid =
      nome !== "" || (showError("#nomeError", "Campo obbligatorio."), false);
    const isCognomeValid =
      cognome !== "" ||
      (showError("#cognomeError", "Campo obbligatorio."), false);
    const isEmailValid = validateField(
      email,
      emailRegex,
      "#emailError",
      "Email non valida.",
    );

    return (
      isNomeValid && isCognomeValid && isEmailValid && validatePasswordFields()
    );
  }

  function togglePassword(id, iconId) {
    const pwdField = document.getElementById(id);
    const icon = $(`#${iconId}`);
    const isPassword = pwdField.type === "password";
    pwdField.type = isPassword ? "text" : "password";
    icon.toggleClass("ti-eye ti-eye-off");
  }

  $("#password, #confirmPassword").on("input", validatePasswordFields);
  $("form").on("submit", validaForm);
  $("#togglePasswordIcon").on("click", () =>
    togglePassword("password", "togglePasswordIcon"),
  );
  $("#toggleConfPasswordIcon").on("click", () =>
    togglePassword("confirmPassword", "toggleConfPasswordIcon"),
  );
});
