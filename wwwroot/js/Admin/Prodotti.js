$(document).ready(function () {
  $(".view-details-button").on("click", function () {
    $("#detailNome").text($(this).data("nome"));
    $("#detailDescrizione").text($(this).data("descrizione"));
    $("#detailPrezzo").text($(this).data("prezzo"));
    $("#detailCategoria").text($(this).data("categoria"));
    $("#detailQuantita").text($(this).data("quantita"));
    $("#detailDisponibilitaNegozi").text($(this).data("disponibilita-negozi"));
    $("#detailsModal").removeClass("hidden");
  });

  $("#closeDetails").on("click", function () {
    $("#detailsModal").addClass("hidden");
  });

  $(".delete-button").on("click", function () {
    const productId = $(this).data("id");
    if (confirm("Sei sicuro di voler eliminare questo prodotto?")) {
      $.ajax({
        url: `/admin/elimina-prodotto/${productId}`,
        type: "POST",
        success: function () {
          location.reload();
        },
        error: function () {
          alert("Errore durante l'eliminazione del prodotto.");
        },
      });
    }
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

  // Add Product Modal
  $("#addProductButton").on("click", function () {
    $("#addProductModal").removeClass("hidden");
  });

  $("#closeAddProductModal").on("click", function () {
    $("#addProductModal").addClass("hidden");
  });

  $("#addProductForm").on("submit", function (e) {
    e.preventDefault();
    const newProduct = {
      Nome: $("#productName").val(),
      Descrizione: $("#productDescription").val(),
      Prezzo: parseFloat($("#productPrice").val()),
      Categoria: $("#productCategory").val(),
      Quantita: parseInt($("#productQuantity").val()),
      DisponibilitaMagazzino: parseInt($("#warehouseAvailability").val()),
      DisponibilitaNegozi: parseInt($("#storeAvailability").val()),
    };

    $.ajax({
      url: "/admin/aggiungi-prodotto",
      type: "POST",
      contentType: "application/json",
      data: JSON.stringify(newProduct),
      success: function () {
        location.reload();
      },
      error: function () {
        alert("Errore durante l'aggiunta del prodotto.");
      },
    });
  });
});
