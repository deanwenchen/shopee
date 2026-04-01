// Figma Product Detail Page - Missing Assets Download Script
// Generated: 2026-04-01

const fs = require('fs');
const path = require('path');
const https = require('https');

const assets = [
  // Most Popular - mp-2.png (was 404 before, try again)
  {
    url: 'https://www.figma.com/api/mcp/asset/8e594e41-038c-4687-a3e9-799c7bad7366',
    name: 'mp-2.png'
  }
];

const outputDir = path.join(__dirname, 'public', 'assets', 'figma');

// Create directory if it doesn't exist
if (!fs.existsSync(outputDir)) {
  fs.mkdirSync(outputDir, { recursive: true });
}

async function downloadAsset(asset) {
  return new Promise((resolve) => {
    const outputPath = path.join(outputDir, asset.name);

    // Skip if already exists
    if (fs.existsSync(outputPath)) {
      console.log(`⏭️  Skipping: ${asset.name} (already exists)`);
      resolve(true);
      return;
    }

    const file = fs.createWriteStream(outputPath);

    https.get(asset.url, (response) => {
      if (response.statusCode === 404) {
        console.log(`❌ Failed: ${asset.name} (404 Not Found)`);
        file.close();
        if (fs.existsSync(outputPath)) {
          fs.unlinkSync(outputPath);
        }
        resolve(true);
        return;
      }

      response.pipe(file);

      file.on('finish', () => {
        file.close();
        console.log(`✅ Downloaded: ${asset.name}`);
        resolve(true);
      });
    }).on('error', (err) => {
      file.close();
      console.log(`❌ Error: ${asset.name} - ${err.message}`);
      resolve(true);
    });
  });
}

async function downloadAll() {
  for (const asset of assets) {
    await downloadAsset(asset);
  }
  console.log('\n✨ Download complete!');
}

downloadAll();
