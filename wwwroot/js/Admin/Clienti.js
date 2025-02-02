$(document).ready(function () {
  let deleteId;

  $(".delete-button").on("click", function () {
    deleteId = $(this).data("id");
    $("#confirmationModal").removeClass("hidden");
  });

  $("#confirmDelete").on("click", function () {
    $.post("/Admin/EliminaAccount", { id: deleteId }, function () {
      location.reload();
    });
  });

  $("#cancelDelete").on("click", function () {
    $("#confirmationModal").addClass("hidden");
  });

  $("#roleFilter").on("change", function () {
    let selectedRole = $(this).val();
    $("#clientiTableBody tr").each(function () {
      let role = $(this).data("role");
      if (selectedRole === "all" || role === selectedRole) {
        $(this).show();
      } else {
        $(this).hide();
      }
    });
  });

  $("#pointsSort").on("change", function () {
    let selectedOrder = $(this).val();
    let rows = $("#clientiTableBody tr").get();
    rows.sort(function (a, b) {
      let pointsA = parseInt($(a).data("points"));
      let pointsB = parseInt($(b).data("points"));
      return selectedOrder === "asc" ? pointsA - pointsB : pointsB - pointsA;
    });
    $.each(rows, function (index, row) {
      $("#clientiTableBody").append(row);
    });
  });

  $("#searchBar").on("input", function () {
    let searchTerm = $(this).val().toLowerCase();
    $("#clientiTableBody tr").each(function () {
      let email = $(this).data("email");
      let nome = $(this).data("nome");
      let cognome = $(this).data("cognome");
      if (
        email.includes(searchTerm) ||
        nome.includes(searchTerm) ||
        cognome.includes(searchTerm)
      ) {
        $(this).show();
      } else {
        $(this).hide();
      }
    });
  });
});
