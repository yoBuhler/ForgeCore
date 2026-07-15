document.addEventListener("DOMContentLoaded", function () {
    // Elementos DOM
    const selectMaterial = document.getElementById("selectMaterial");
    const containerConversion = document.getElementById("containerConversion");
    const inputValue = document.getElementById("inputValue");
    const selectFrom = document.getElementById("selectFrom");
    const selectTo = document.getElementById("selectTo");
    const btnConvert = document.getElementById("btnConvert");

    const resultPanel = document.getElementById("resultPanel");
    const resConvertedValue = document.getElementById("resConvertedValue");

    const errorPanel = document.getElementById("errorPanel");

    // Pegando o Antiforgery Token gerado pelo Razor
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    // -------------------------------------------------------------
    // Alteração do Material Selecionado
    // -------------------------------------------------------------
    selectMaterial.addEventListener("change", async function () {
        const materialId = this.value;

        // Esconde as áreas de input e resultados ao trocar de material
        containerConversion.style.display = "none";
        closePanels();

        if (!materialId) return;

        try {
            // Chamada GET para buscar as unidades de medida disponíveis
            const response = await fetch(
                `/Conversion/GetMeasurementUnitByMaterialIdAsync/${materialId}`, {
                headers: {
                    "Content-Type": "application/json",
                    "RequestVerificationToken": token // Proteção CSRF obrigatória do ASP.NET Core
                }
            });

            if (!response.ok) throw new Error("Erro ao carregar as unidades.");

            const units = await response.json();

            // Preenche os selects de DE e PARA
            fillSelectUnits(selectFrom, units);
            fillSelectUnits(selectTo, units);

            // Exibe a área de inputs
            containerConversion.style.display = "block";

        } catch (error) {
            showError("Ocorreu um erro ao carregar as unidades para este material.");
            console.error(error);
        }
    });

    // -------------------------------------------------------------
    // Clique no Botão Converter
    // -------------------------------------------------------------
    btnConvert.addEventListener("click", async function () {
        closePanels();

        const materialId = selectMaterial.value;
        const value = parseFloat(inputValue.value);
        const unitFromId = selectFrom.value;
        const unitToId = selectTo.value;

        // Validações básicas no Client-side
        if (isNaN(value) || value < 0) {
            showError("Por favor, insira um valor válido.");
            return;
        }

        // Montagem do payload conforme a classe RequestConversionCalcViewModel
        const payload = {
            materialId: materialId,
            unitFromId: unitFromId,
            unitToId: unitToId,
            value: value
        };

        try {
            // Chamada POST enviando JSON com os dados e o Header de Segurança
            const response = await fetch("/Conversion/Converter", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "RequestVerificationToken": token // Proteção CSRF obrigatória do ASP.NET Core
                },
                body: JSON.stringify(payload)
            });

            if (!response.ok) throw new Error("Erro ao processar cálculo no servidor.");

            const jsonResponse = await response.json();

            if (jsonResponse.success) {
                // Exibe os resultados formatados na tela
                resConvertedValue.textContent = `${jsonResponse.convertedValue.toFixed(3)}`;
                resultPanel.style.display = "block";
            } else {
                showError(jsonResponse.message || "Não foi possível realizar esta conversão.");
            }

        } catch (error) {
            showError("Ocorreu um erro interno de rede ou servidor ao realizar o cálculo.");
            console.error(error);
        }
    });

    // -------------------------------------------------------------
    // FUNÇÕES AUXILIARES
    // -------------------------------------------------------------
    function fillSelectUnits(selectElement, units) {
        selectElement.innerHTML = ""; // Limpa opções anteriores
        units.forEach(unit => {
            const opt = document.createElement("option");
            opt.value = unit.id;
            opt.textContent = `${unit.id} - ${unit.name}`;
            selectElement.appendChild(opt);
        });
    }

    function showError(message) {
        errorPanel.textContent = message;
        errorPanel.classList.remove("d-none");
    }

    function closePanels() {
        resultPanel.style.display = "none";
        errorPanel.classList.add("d-none");
        errorPanel.textContent = "";
    }
});