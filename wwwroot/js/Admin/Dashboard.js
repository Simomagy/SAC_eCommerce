$(document).ready(function () {
  console.log("DOM fully loaded and parsed");

  const $button = $("#menu-button");
  const $dropdown = $("#menu-dropdown");

  $button.on("click", function (e) {
    e.stopPropagation();
    $dropdown.toggleClass("hidden");
  });

  $(document).on("click", function () {
    if (!$dropdown.hasClass("hidden")) {
      $dropdown.addClass("hidden");
    }
  });

  const orders = window.ordersData;
  if (!orders) {
    console.error("Orders data is null or undefined");
    return;
  }

  const ordersByWeek = {};

  orders.forEach((order) => {
    const orderDate = new Date(order.Data);
    const weekNumber = `${orderDate.getFullYear()}-W${Math.ceil((orderDate.getDate() - 1) / 7)}`;
    if (!ordersByWeek[weekNumber]) {
      ordersByWeek[weekNumber] = 0;
    }
    ordersByWeek[weekNumber]++;
  });

  const labels = Object.keys(ordersByWeek);
  const data = Object.values(ordersByWeek);

  const ctx = $("#ordersChart")[0].getContext("2d");
  Chart.defaults.backgroundColor = "#27272a"; // tailwind zinc 800
  new Chart(ctx, {
    type: "line",
    data: {
      labels: labels,
      datasets: [
        {
          label: "Numero di ordini",
          data: data,
          borderColor: "#EB5E28", // primary color
          backgroundColor: "rgba(235, 94, 40, 0.2)", // primary color with transparency
          fill: true,
          tension: 0.4, // makes the line more rounded
          pointBackgroundColor: "#EB5E28", // primary color
          pointBorderColor: "#EB5E28", // primary color
          pointRadius: 5, // larger points
          pointHoverRadius: 7, // larger hover points
        },
      ],
    },
    options: {
      scales: {
        x: {
          title: {
            display: true,
            text: "Settimana",
            color: "#FFFFFF", // white color for text
          },
          ticks: {
            color: "#FFFFFF", // white color for ticks
          },
        },
        y: {
          title: {
            display: true,
            text: "Numero di ordini",
            color: "#FFFFFF", // white color for text
          },
          ticks: {
            color: "#FFFFFF", // white color for ticks
          },
        },
      },
      plugins: {
        legend: {
          labels: {
            color: "#FFFFFF", // white color for legend,
          },
        },
      },
    },
  });
});
