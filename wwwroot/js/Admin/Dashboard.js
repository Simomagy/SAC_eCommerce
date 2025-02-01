document.addEventListener("DOMContentLoaded", () => {
  const button = document.getElementById("menu-button");
  const dropdown = document.getElementById("menu-dropdown");

  button.addEventListener("click", (e) => {
    e.stopPropagation();
    dropdown.classList.toggle("hidden");
  });

  document.addEventListener("click", () => {
    if (!dropdown.classList.contains("hidden")) {
      dropdown.classList.add("hidden");
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

  const ctx = document.getElementById("ordersChart").getContext("2d");
  new Chart(ctx, {
    type: "line",
    data: {
      labels: labels,
      datasets: [
        {
          label: "Numero di ordini",
          data: data,
          borderColor: "rgba(75, 192, 192, 1)",
          backgroundColor: "rgba(75, 192, 192, 0.2)",
          fill: true,
        },
      ],
    },
    options: {
      scales: {
        x: {
          title: {
            display: true,
            text: "Settimana",
          },
        },
        y: {
          title: {
            display: true,
            text: "Numero di ordini",
          },
        },
      },
    },
  });
});
