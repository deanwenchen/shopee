// Test script to verify login API from frontend context
import axios from 'axios';

const API_BASE_URL = 'http://localhost:9000/api/auth';

async function testLoginStep1() {
  console.log('=== Testing login-step1 API ===');
  console.log('Email:', 'deanwenchen@gmail.com');

  try {
    const response = await axios.post(
      `${API_BASE_URL}/login-step1`,
      { email: 'deanwenchen@gmail.com' },
      {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true
      }
    );

    console.log('✅ Success!');
    console.log('Response:', response.data);
    console.log('CORS Headers:', response.headers);
    return true;
  } catch (error) {
    console.error('❌ Error:', error.message);
    if (error.response) {
      console.error('Response status:', error.response.status);
      console.error('Response data:', error.response.data);
      console.error('CORS Headers:', error.response.headers);
    } else if (error.request) {
      console.error('No response received - network/CORS issue');
    }
    return false;
  }
}

testLoginStep1();
