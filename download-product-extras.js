const https = require('https');
const fs = require('fs');
const path = require('path');

const outputDir = path.join(__dirname, 'frontend', 'public', 'assets', 'figma');

if (!fs.existsSync(outputDir)) {
  fs.mkdirSync(outputDir, { recursive: true });
}

// Product detail page additional resources from Figma Page 37
const resources = {
  // You Might Like products (4 items)
  'yl-1.png': 'https://www.figma.com/api/mcp/asset/4b272afc-2453-4566-8dad-8b886a3344ec',
  'yl-2.png': 'https://www.figma.com/api/mcp/asset/549512cf-4104-4a0d-9ab4-c834155c9d4b',
  'yl-3.png': 'https://www.figma.com/api/mcp/asset/af9e548f-1793-4409-906e-153f0a91fe73',
  'yl-4.png': 'https://www.figma.com/api/mcp/asset/4c8354ff-c590-490f-a217-4327a959a368',

  // Most Popular products (4 items)
  'mp-1.png': 'https://www.figma.com/api/mcp/asset/5ec21bec-b10a-4371-ad81-a391d5c5320a',
  'mp-2.png': 'https://www.figma.com/api/mcp/asset/d9337b16-f5cc-4e4d-a01e-5a5b38fa48f4',
  'mp-3.png': 'https://www.figma.com/api/mcp/asset/df68db9d-3222-44b0-bd4e-d6214981de26',
  'mp-4.png': 'https://www.figma.com/api/mcp/asset/59cc0151-a4da-48fc-a3e6-a82776572ea0',

  // Review avatar images
  'review-avatar-1.png': 'https://www.figma.com/api/mcp/asset/1fa10e8b-88ca-40c4-baa0-746141796392',
  'review-avatar-2.png': 'https://www.figma.com/api/mcp/asset/a1b2e1be-2c46-4cf4-a219-b0d979a30846',

  // Icons
  'star-filled.svg': 'https://www.figma.com/api/mcp/asset/9df8f35a-c189-4616-a165-e3e34130d906',
  'star-half.svg': 'https://www.figma.com/api/mcp/asset/ac074f71-67bc-42c4-89df-b0ce5525e0b7',
  'star-empty.svg': 'https://www.figma.com/api/mcp/asset/9c3c4420-5898-41e4-9ee5-f2d6f833f433',
};

function downloadFile(url, filename) {
  return new Promise((resolve, reject) => {
    const filePath = path.join(outputDir, filename);
    https.get(url, (response) => {
      if (response.statusCode !== 200) {
        console.error(`Failed to download ${filename}: ${response.statusCode}`);
        resolve({ filename, success: false, error: response.statusCode });
        return;
      }
      const file = fs.createWriteStream(filePath);
      response.pipe(file);
      file.on('finish', () => {
        file.close();
        console.log(`Downloaded: ${filename}`);
        resolve({ filename, success: true });
      });
    }).on('error', (err) => {
      fs.unlink(filePath, () => {});
      console.error(`Error downloading ${filename}: ${err.message}`);
      resolve({ filename, success: false, error: err.message });
    });
  });
}

async function downloadAll() {
  console.log(`Downloading ${Object.keys(resources).length} resources to ${outputDir}...`);
  const results = [];
  for (const [filename, url] of Object.entries(resources)) {
    const result = await downloadFile(url, filename);
    results.push(result);
  }
  const success = results.filter(r => r.success);
  const failed = results.filter(r => !r.success);
  console.log(`\nDownload complete! Success: ${success.length}, Failed: ${failed.length}`);
  if (failed.length > 0) {
    console.log('Failed files:');
    failed.forEach(f => console.log(`  - ${f.filename}: ${f.error}`));
  }
}

downloadAll();
