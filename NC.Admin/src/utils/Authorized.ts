import RenderAuthorize from '@/components/Authorized';
import { getAuthority } from './authority';

let Authorized = RenderAuthorize(getAuthority());

const reloadAuthorized = (): void => {
  Authorized = RenderAuthorize(getAuthority());
}

export { reloadAuthorized };
export default Authorized;



