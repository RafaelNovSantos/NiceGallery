window.exportReportToPdf = (produtos) => {
    const doc = new jsPDF();

    const headers = [["Nome", "Preço"]];
    const rows = produtos.map(p => [p.name, "R$ " + p.price.toFixed(2)]);

    doc.autoTable({
        head: headers,
        body: rows,
    });

    doc.save("relatorio.pdf");
};
