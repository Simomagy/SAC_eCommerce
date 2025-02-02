$(document).ready(function () {
  $(".view-details-button").on("click", function () {
    $("#detailNome").text($(this).data("nome"));
    $("#detailDescrizione").text($(this).data("descrizione"));
    $("#detailPrezzo").text($(this).data("prezzo"));
    $("#detailCategoria").text($(this).data("categoria"));
    $("#detailQuantita").text($(this).data("quantita"));
    $("#detailDisponibilitaNegozi").text($(this).data("disponibilita-negozi"));
    console.log($(this).data("disponibilita-negozi"));
    $("#detailsModal").removeClass("hidden");
  });

  $("#closeDetails").on("click", function () {
    $("#detailsModal").addClass("hidden");
  });

  $("#searchBar").on("input", function () {
    let searchTerm = $(this).val().toLowerCase();
    $("#prodottiTableBody tr").each(function () {
      let nome = $(this).data("nome");
      let categoria = $(this).data("categoria");
      if (nome.includes(searchTerm) || categoria.includes(searchTerm)) {
        $(this).show();
      } else {
        $(this).hide();
      }
    });
  });

  $("#sortBy, #sortOrder").on("change", function () {
    let sortBy = $("#sortBy").val();
    let sortOrder = $("#sortOrder").val();
    let rows = $("#prodottiTableBody tr").get();
    rows.sort(function (a, b) {
      let valA = $(a).data(sortBy.toLowerCase());
      let valB = $(b).data(sortBy.toLowerCase());
      if (sortBy === "Prezzo") {
        valA = parseFloat(valA);
        valB = parseFloat(valB);
      }
      if (sortOrder === "asc") {
        return valA > valB ? 1 : -1;
      } else {
        return valA < valB ? 1 : -1;
      }
    });
    $.each(rows, function (index, row) {
      $("#prodottiTableBody").append(row);
    });
  });
});
