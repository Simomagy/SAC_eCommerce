$(document).ready(function () {
  $(".view-details-button").on("click", function () {
    $("#detailData").text($(this).data("data"));
    $("#detailCliente").text($(this).data("cliente"));
    $("#detailStato").text($(this).data("stato"));
    $("#detailTotale").text($(this).data("totale"));

    // Clear previous product details
    $("#detailProdotti").empty();

    // Fetch product details
    let prodotti = $(this).data("prodotti");

    if (prodotti && prodotti.length) {
      prodotti.forEach((prodotto) => {
        let totaleProdotto =
          parseFloat(prodotto.Quantita) * parseFloat(prodotto.Prezzo);
        $("#detailProdotti").append(`
          <tr>
            <td class="px-4 py-3">${prodotto.Nome}</td>
            <td class="px-4 py-3">${prodotto.Quantita}</td>
            <td class="px-4 py-3">${prodotto.Prezzo}</td>
            <td class="px-4 py-3">${totaleProdotto.toFixed(2)}</td>
          </tr>
        `);
      });
    }

    $("#detailsModal").removeClass("hidden");
  });

  $("#closeDetails").on("click", function () {
    $("#detailsModal").addClass("hidden");
  });

  $(".delete-button").on("click", function () {
    const orderId = $(this).data("id");
    if (confirm("Sei sicuro di voler eliminare questo ordine?")) {
      $.ajax({
        url: `/admin/elimina-ordine/${orderId}`,
        type: "POST",
        success: function () {
          location.reload();
        },
        error: function () {
          alert("Errore durante l'eliminazione dell'ordine.");
        },
      });
    }
  });

  $("#searchBar").on("input", function () {
    let searchTerm = $(this).val().toLowerCase();
    $("#ordiniTableBody tr").each(function () {
      let cliente = $(this).data("cliente");
      let stato = $(this).data("stato");
      if (cliente.includes(searchTerm) || stato.includes(searchTerm)) {
        $(this).show();
      } else {
        $(this).hide();
      }
    });
  });

  $("#sortBy, #sortOrder").on("change", function () {
    let sortBy = $("#sortBy").val();
    let sortOrder = $("#sortOrder").val();
    let rows = $("#ordiniTableBody tr").get();
    rows.sort(function (a, b) {
      let valA = $(a).data(sortBy.toLowerCase());
      let valB = $(b).data(sortBy.toLowerCase());
      if (sortBy === "Totale") {
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
      $("#ordiniTableBody").append(row);
    });
  });
});
