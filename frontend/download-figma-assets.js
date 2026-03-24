import fs from 'fs';
import path from 'path';
import https from 'https';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

// 从 Vue 文件中提取所有 Figma asset URL
const vueFiles = [
  'src/views/Password.vue',
  'src/views/PasswordRecovery.vue',
  'src/views/PasswordRecoveryCode.vue',
  'src/views/NewPassword.vue',
  'src/views/HelloCard.vue',
  'src/views/LoginPage.vue',
  'src/views/CreateAccount.vue',
  'src/views/StartPage.vue'
];

// 存储所有唯一的 Figma URL
const figmaUrls = new Set();

vueFiles.forEach(file => {
  const filePath = path.join(__dirname, file);
  if (fs.existsSync(filePath)) {
    const content = fs.readFileSync(filePath, 'utf-8');
    const matches = content.match(/https:\/\/www\.figma\.com\/api\/mcp\/asset\/[a-f0-9-]+/g);
    if (matches) {
      matches.forEach(url => figmaUrls.add(url));
    }
  }
});

console.log(`Found ${figmaUrls.size} unique Figma URLs`);

// 创建 assets 目录
const assetsDir = path.join(__dirname, 'src/assets/figma');
if (!fs.existsSync(assetsDir)) {
  fs.mkdirSync(assetsDir, { recursive: true });
}

// 下载函数
function downloadFile(url, dest) {
  return new Promise((resolve, reject) => {
    const file = fs.createWriteStream(dest);
    https.get(url, (response) => {
      if (response.statusCode !== 200) {
        reject(new Error(`Failed to download: ${response.statusCode}`));
        return;
      }
      response.pipe(file);
      file.on('finish', () => {
        file.close();
        resolve();
      });
    }).on('error', (err) => {
      fs.unlink(dest, () => {});
      reject(err);
    });
  });
}

// 下载所有文件
async function downloadAll() {
  const urls = Array.from(figmaUrls);
  const results = [];

  for (let i = 0; i < urls.length; i++) {
    const url = urls[i];
    const assetId = url.split('/').pop();
    const fileName = `${assetId}.png`;
    const dest = path.join(assetsDir, fileName);

    console.log(`[${i + 1}/${urls.length}] Downloading: ${assetId}`);

    try {
      await downloadFile(url, dest);
      results.push({ url, localPath: `@/assets/figma/${fileName}`, success: true });
      console.log(`  ✓ Downloaded`);
    } catch (err) {
      results.push({ url, error: err.message, success: false });
      console.log(`  ✗ Failed: ${err.message}`);
    }
  }

  // 生成映射文件
  fs.writeFileSync(path.join(__dirname, 'src/assets/figma-url-map.json'), JSON.stringify(results, null, 2));
  console.log('\nMap file generated: src/assets/figma-url-map.json');
}

downloadAll().catch(console.error);
