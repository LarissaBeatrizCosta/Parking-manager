const addButton = document.getElementById("addButton");
const finalizeButton = document.getElementById("finalizeButton");
const plateInput = document.getElementById("input-plate");

addButton.onclick = async function () {
  const plate = plateInput.value.trim();
  const validation = validatePlate(plate);
  if (!validation.isOk) {
    alert(validation.message);
    return;
  }
  const result = await fetchParkingSessions(`/ViewParking/${plate}`, "POST");
  alert(result.message);
  if (result.isOk) {
    window.location.reload();
  }
};

finalizeButton.onclick = async function () {
  const plate = plateInput.value.trim();
  const validation = validatePlate(plate);
  if (!validation.isOk) {
    alert(validation.message);
    return;
  }
  const result = await fetchParkingSessions(
    `/ViewParking/${plate}/finalize`,
    "PUT"
  );
  alert(result.message);
  if (result.isOk) {
    window.location.reload();
  }
};

async function fetchParkingSessions(url, method) {
  try {
    const response = await fetch(url, { method: method });
    const responseText = await response.text();

    if (response.ok && responseText) {
      return JSON.parse(responseText);
    } else {
      return {
        isOk: false,
        message: responseText || "Erro ao realizar operação.",
      };
    }
  } catch (e) {
    console.error(e);
    return { isOk: false, message: "Erro ao realizar a requisição." };
  }
}

function validatePlate(plate) {
  if (!plate) {
    return { isOk: false, message: "A Placa é obrigatória. Tente novamente." };
  }
  return { isOk: true };
}
