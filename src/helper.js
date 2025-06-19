export function FormatPipeTable(input) {
    const rows = input
        .trim()
        .split('\n')
        .map(line =>
            line
                .trim()
                .slice(1, -1) // remove leading/trailing pipes
                .split('|')
                .map(cell => cell.trim())
        );

    // Step 1: Merge all columns after the first into a single column
    const mergedRows = rows.map(cols => {
        const first = cols[0] || '';
        const second = cols.slice(1).join(' ').trim();
        return [first, second];
    });

    // Step 2: Find max width for each of the two columns
    const colWidths = [0, 0];
    mergedRows.forEach(([first, second]) => {
        colWidths[0] = Math.max(colWidths[0], first.length);
        colWidths[1] = Math.max(colWidths[1], second.length);
    });

    // Step 3: Format each row with correct padding so the last pipe aligns
    const formattedRows = mergedRows.map(([first, second]) => {
        const col1 = first.padEnd(colWidths[0]);
        const col2 = second.padEnd(colWidths[1]);
        return `| ${col1} | ${col2} |`;
    });

    return formattedRows.join('\n');
}