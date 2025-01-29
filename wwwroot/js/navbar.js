$(document).ready(function () {
  function showDropdown(dropdown) {
    gsap.fromTo(
      dropdown,
      { opacity: 0, display: "none" },
      { duration: 0.5, opacity: 1, display: "block" },
    );
    dropdown.removeClass("hidden");
  }

  function hideDropdown(dropdown) {
    gsap.fromTo(
      dropdown,
      { opacity: 1, display: "block" },
      { duration: 0.5, opacity: 0, display: "none" },
    );
    dropdown.addClass("hidden");
  }

  $("#appsButton, #appsDropdown").hover(
    function () {
      showDropdown($("#appsDropdown"));
    },
    function () {
      hideDropdown($("#appsDropdown"));
    },
  );

  $("#userButton, #userDropdown").hover(
    function () {
      showDropdown($("#userDropdown"));
    },
    function () {
      hideDropdown($("#userDropdown"));
    },
  );
});
